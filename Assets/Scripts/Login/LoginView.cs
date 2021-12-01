using UnityEngine; using UnityEngine.UI;  using Sirenix.OdinInspector; using System; using UnityEngine.SceneManagement;

using UniRx;
using Newtonsoft.Json.Linq;

public class LoginView : MonoBehaviour {     bool isPhonePwd = true;     Canvas canvas;     Button btnBack;     Button btnSend;     InputField inputName;     InputField inputPwd;     Button btnSubmit;     Text btnSubmitText;     Button btnSwitch;     Text inputPwdHint;     Text btnSwitchText;      [LabelText("发送验证码文本")]     public Text btnSendText;      
    private IDisposable mIntervalDisable;
    public int INTERVAL_SECONDS = 60;      // Start is called before the first frame update     void Start()     {         canvas = GameObject.Find("Canvas").GetComponent<Canvas>();         btnBack = GameObject.Find("Canvas/BtnBack").GetComponent<Button>();         btnSend = GameObject.Find("Canvas/InputPassword/BtnSend").GetComponent<Button>();         btnSend.gameObject.SetActive(!isPhonePwd);                   inputName = GameObject.Find("Canvas/InputName").GetComponent<InputField>();         inputPwd = GameObject.Find("Canvas/InputPassword").GetComponent<InputField>();         btnSubmit = GameObject.Find("Canvas/BtnSubmit").GetComponent<Button>();         btnSubmitText = GameObject.Find("Canvas/BtnSubmit/Text").GetComponent<Text>();          btnSwitch = GameObject.Find("Canvas/BtnSwitch").GetComponent<Button>();         inputPwdHint = GameObject.Find("Canvas/InputPassword/Placeholder").GetComponent<Text>();         btnSwitchText = GameObject.Find("Canvas/BtnSwitch/Text").GetComponent<Text>();
        
    }      /// <summary>     /// 返回     /// </summary>     public void OnBackPress()     {         SceneManager.LoadScene("Realm");     }      /// <summary>     /// 检查是否可登录     /// </summary>     public void CheckEnableLogin()     {         string name = inputName.text;         string password = inputPwd.text;         bool enableSubmit = (name.Length > 0 && password.Length > 0);         btnSubmit.enabled = enableSubmit;         btnSubmit.interactable = enableSubmit;         Color nowColor;         string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";         ColorUtility.TryParseHtmlString(color, out nowColor);         btnSubmitText.color = nowColor;     }      /// <summary>
    /// 检查发送验证码是否可用
    /// </summary>     public void OnCheckSendStatus()
    {
        string name = inputName.text;
        bool status = !string.IsNullOrEmpty(name) && name.Length == 11 && mIntervalDisable==null;
        btnSend.enabled = status;
        btnSend.interactable = status;
    }

    /// <summary>     /// 切换     /// </summary>     public void OnSwitchClick()     {         isPhonePwd = !isPhonePwd;         btnSwitchText.text = isPhonePwd ? "切换验证码登录 ":"切换密码登录";         inputPwdHint.text = isPhonePwd ? "请输入6位数密码" : "请输入验证码";         btnSend.gameObject.SetActive(!isPhonePwd);         inputPwd.text = "";         inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;         inputPwd.characterLimit = isPhonePwd ? 6 : 4;         if (!isPhonePwd)
        {
            OnFinishInterval();
        }

    }

    ///// <summary>
    ///// 进入设置页
    ///// </summary>
    //public void EnterSetting()
    //{
    //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
    //    jo.Call("sendUserInfo", "18811112222");
    //    jo.Call("sendUpdateInfo", 1);
    //    jo.Call("enterSettingActivity");
    //}

    /// <summary>     /// 点击发送验证码     /// </summary>      public void OnSendVerifyCode()     {
        string phoneNum = inputName.text;
        if (string.IsNullOrEmpty(phoneNum))
        {
            Debug.Log("OnSendVerifyCode  inputName is empty");
            return;
        }
        //发送验证码前先进行腾讯滑块二次验证
        AndroidJavaObject jo = new AndroidJavaObject("com.roobo.pudding.ai.TencentCaptchaPlugin");
        jo.Call("openTencentCapture", "2099959054", "roobo-tencent-captcha", true, "Canvas", "OnTencentCaptchaBack");
    }

    /// <summary>
    /// 从腾讯滑块验证返回的结果json
    /// </summary>
    /// <param name="json"></param>
    public void OnTencentCaptchaBack(string json)
    {

        string phoneNum = inputName.text;
        if (string.IsNullOrEmpty(phoneNum)){
            Debug.Log("OnTencentCaptchaBack=inputName is empty");
            return;
        }
        Debug.Log("OnTencentCaptchaBack=" + json);
        TencentCaptchaResult result = JsonUtility.FromJson<TencentCaptchaResult>(json);
        Debug.Log("OnTencentCaptchaBack=" + result.method);
        if(result.method.Equals("onSuccess"))
        {
            OnTencentCaptchaSuccess(phoneNum,result.data);
        }
        else if (result.method.Equals("onFail"))
        {

        }
    }


    /// <summary>
    /// 验证成功
    /// </summary>
    /// <param name="phoneNum"></param>
    /// <param name="data"></param>
    public void OnTencentCaptchaSuccess(string phoneNum,string data) {
        CaptchaRequestBean captchaBean = JsonUtility.FromJson<CaptchaRequestBean>(data);

        AuthCodeRequest request = new AuthCodeRequest(phoneNum, captchaBean);
        Debug.Log("OnTencentCaptchaBack=OnTencentCaptchaSuccess" + data);
        Debug.Log("OnTencentCaptchaBack=AuthCodeRequest-json" + JsonUtility.ToJson(request));
        HttpRx.Post("/pudding/teacher/v1/user/getAuthcodeWithCaptcha", request).Subscribe((r) =>
        {
            Debug.Log("OnTencentCaptchaBack=发送成功" + r);
            btnSend.enabled = false;
            btnSend.interactable = false;
            OperatorSendTextColor(false);

            mIntervalDisable = Observable.Interval(System.TimeSpan.FromSeconds(1)).Take(INTERVAL_SECONDS).Subscribe(timer =>
            {
                Debug.Log("OnTencentCaptchaBack=倒计时:" + timer);
                btnSendText.text = (INTERVAL_SECONDS - (timer + 1)) + "s后重发";
                if (timer >= INTERVAL_SECONDS-1)
                {
                    OnFinishInterval();
                }
            }).AddTo(this);
        }, (e) => {
            Debug.Log("OnTencentCaptchaBack=exception:" + (e as HttpError).message+" code:"+ (e as HttpError).code);
        }).AddTo(this);
    }

    /// <summary>
    /// 完成倒计时,重置
    /// </summary>
    private void OnFinishInterval()
    {
        if (mIntervalDisable != null)
        {
            mIntervalDisable.Dispose();
            mIntervalDisable = null;
        }
        btnSendText.text = "发送验证码";
        bool enable = !string.IsNullOrEmpty(inputName.text) && inputName.text.Length == 11;
        btnSend.enabled = enable;
        btnSend.interactable = enable;
        OperatorSendTextColor(enable);
    }

    private void OperatorSendTextColor(bool enable)
    {
        Color nowColor;
        string color = enable? "#DB6A07": "#FFFFFF";
        ColorUtility.TryParseHtmlString(color, out nowColor);
        btnSendText.color = nowColor;
    }

    /// <summary>
    /// 滑块验证结果
    /// </summary>
    [System.Serializable]
    class TencentCaptchaResult
    {
        public string method;
        public string data;

        public TencentCaptchaResult(string method, string data)
        {
            this.method = method;
            this.data = data;
        }
    }

    [System.Serializable]
    class TencentCaptchaData
    {
        public int ret;
        public string ticket;
        public string randstr;
        public string bizState;
        public string appid;

        public TencentCaptchaData(int ret, string ticket, string randstr, string bizState, string appid)
        {
            this.ret = ret;
            this.ticket = ticket;
            this.randstr = randstr;
            this.bizState = bizState;
            this.appid = appid;
        }
    }

    /// <summary>
    /// 获取验证码请求参数
    /// </summary>
    [System.Serializable]
    class AuthCodeRequest
    {
        public string phone;
        public CaptchaRequestBean captchaCheck;
        public string pCode = "+86";
        public string lang = "zh";
        public string allow = "authcode-login";
        

        public AuthCodeRequest(string phone, CaptchaRequestBean captchaCheck, string pCode= "+86", string lang = "zh", string allow = "authcode-login")
        {
            this.phone = phone;
            this.pCode = pCode;
            this.lang = lang;
            this.allow = allow;
            this.captchaCheck = captchaCheck;
        }
    }


    [System.Serializable]
    class CaptchaRequestBean
    {
        public string ticket;
        public string randstr;
        public string captchatype = "app";

        public CaptchaRequestBean(string ticket,string randstr,string captchatype = "app")
        {
            this.ticket = ticket;
            this.randstr = randstr;
            this.captchatype = captchatype;
        }
    }

    /// <summary>     /// login     /// </summary>     public void OnLoginSubmit()     {         string name = inputName.text;         string pwd = inputPwd.text;         if (isPhonePwd)         {                     }         else         {                      }     }      /// <summary>     ///      /// </summary>     public void OnPrivacyClick()     {         Application.OpenURL("https://activity.roobovip.com/#/privacy");     }      /// <summary>     ///     /// </summary>     public void OnUserAgreement()     {         Application.OpenURL("https://activity.roobovip.com/#/userAgreement");     }      private void Update()
    {
        if (!isPhonePwd)
        {
            if (mIntervalDisable != null)
            {
                return;
            }
            btnSendText.text = "发送验证码";
            bool enable = !string.IsNullOrEmpty(inputName.text) && inputName.text.Length == 11;
            btnSend.enabled = enable;
            btnSend.interactable = enable;
            OperatorSendTextColor(enable);
        }
    } } 
