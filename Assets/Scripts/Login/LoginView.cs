using UnityEngine; using UnityEngine.UI;  using Sirenix.OdinInspector; using System; using UnityEngine.SceneManagement;

using UniRx;
using Newtonsoft.Json.Linq;

public class LoginView : MonoBehaviour {     bool isPhonePwd = true;     Canvas canvas;     Button btnBack;     Button btnSend;     InputField inputName;     InputField inputPwd;     Button btnSubmit;     Text btnSubmitText;     Button btnSwitch;     Text inputPwdHint;     Text btnSwitchText;      [LabelText("发送验证码文本")]     public Text btnSendText;       // Start is called before the first frame update     void Start()     {         canvas = GameObject.Find("Canvas").GetComponent<Canvas>();         btnBack = GameObject.Find("Canvas/BtnBack").GetComponent<Button>();         btnSend = GameObject.Find("Canvas/InputPassword/BtnSend").GetComponent<Button>();         btnSend.gameObject.SetActive(!isPhonePwd);          inputName = GameObject.Find("Canvas/InputName").GetComponent<InputField>();         inputPwd = GameObject.Find("Canvas/InputPassword").GetComponent<InputField>();         btnSubmit = GameObject.Find("Canvas/BtnSubmit").GetComponent<Button>();         btnSubmitText = GameObject.Find("Canvas/BtnSubmit/Text").GetComponent<Text>();          btnSwitch = GameObject.Find("Canvas/BtnSwitch").GetComponent<Button>();         inputPwdHint = GameObject.Find("Canvas/InputPassword/Placeholder").GetComponent<Text>();         btnSwitchText = GameObject.Find("Canvas/BtnSwitch/Text").GetComponent<Text>();        }      /// <summary>     /// 锟斤拷锟截帮拷钮     /// </summary>     public void OnBackPress()     {         SceneManager.LoadScene("Realm");     }      /// <summary>     /// 锟斤拷锟斤拷欠锟缴碉拷锟斤拷锟铰?     /// </summary>     public void CheckEnableLogin()     {         string name = inputName.text;         string password = inputPwd.text;         bool enableSubmit = (name.Length > 0 && password.Length > 0);         btnSubmit.enabled = enableSubmit;         btnSubmit.interactable = enableSubmit;         Color nowColor;         string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";         ColorUtility.TryParseHtmlString(color, out nowColor);         btnSubmitText.color = nowColor;     }      /// <summary>     /// 切换     /// </summary>     public void OnSwitchClick()     {         isPhonePwd = !isPhonePwd;         btnSwitchText.text = isPhonePwd ? "切换验证码登录 ":"切换密码登录";         inputPwdHint.text = isPhonePwd ? "请输入6位数密码" : "请输入验证码";         btnSend.gameObject.SetActive(!isPhonePwd);         inputPwd.text = "";         inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;         inputPwd.characterLimit = isPhonePwd ? 6 : 4;     }

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
        //string json = "{\n" +
        //    "    \"method\":\"onSuccess\",\n" +
        //    "    \"data\":{\n" +
        //    "        \"ret\":0,\n" +
        //    "        \"ticket\":\"t03UApjWlUk5vfQOzQ_aCO4USxnpu5iFH797OKv6y2owszyWaW3QDsIpOeXPnH4DBJjwzbkbCU3FFXHaY8Ybu2KvlKLEkU2Mltwn8DcFbJErbs*\",\n" +
        //    "        \"randstr\":\"@WOc\",\n" +
        //    "        \"bizState\":\"roobo-tencent-captcha\",\n" +
        //    "        \"appid\":\"2099959054\"\n" +
        //    "    }\n" +
        //    "}";
        //OnTencentCaptchaBack(json);
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
            Observable.Interval(System.TimeSpan.FromSeconds(1)).TakeUntil(Observable.Timer(TimeSpan.FromMinutes(1))).Subscribe(timer =>
            {
                Debug.Log("OnTencentCaptchaBack=倒计时:" + timer);
                btnSendText.text = timer + "s后重发";
                Debug.Log("OnTencentCaptchaBack=倒计时:gasdfasdfs" + timer);
            }).AddTo(this);
        }, (e) => {
            Debug.Log("OnTencentCaptchaBack=exception:" + (e as HttpError).message);
            Debug.Log("OnTencentCaptchaBack=exception:" + (e as HttpError).code);
        }).AddTo(this);
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

    /// <summary>     /// login     /// </summary>     public void OnLoginSubmit()     {         string name = inputName.text;         string pwd = inputPwd.text;         if (isPhonePwd)         {                     }         else         {                      }     }      /// <summary>     ///      /// </summary>     public void OnPrivacyClick()     {      }      /// <summary>     ///     /// </summary>     public void OnUserAgreement()     {      } } 
