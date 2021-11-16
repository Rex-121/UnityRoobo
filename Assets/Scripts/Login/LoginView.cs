using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    bool isPhonePwd = true;
    Canvas canvas;
    Button btnBack;
    Button btnSend;
    InputField inputName;
    InputField inputPwd;
    Button btnSubmit;
    Text btnSubmitText;
    Button btnSwitch;
    Text inputPwdHint;
    Text btnSwitchText;
    Text btnSendText;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        btnBack = GameObject.Find("Canvas/BtnBack").GetComponent<Button>();
        btnSend = GameObject.Find("Canvas/InputPassword/BtnSend").GetComponent<Button>();
        btnSend.gameObject.SetActive(!isPhonePwd);

        inputName = GameObject.Find("Canvas/InputName").GetComponent<InputField>();
        inputPwd = GameObject.Find("Canvas/InputPassword").GetComponent<InputField>();
        btnSubmit = GameObject.Find("Canvas/BtnSubmit").GetComponent<Button>();
        btnSubmitText = GameObject.Find("Canvas/BtnSubmit/Text").GetComponent<Text>();

        btnSwitch = GameObject.Find("Canvas/BtnSwitch").GetComponent<Button>();
        inputPwdHint = GameObject.Find("Canvas/InputPassword/Placeholder").GetComponent<Text>();
        btnSwitchText = GameObject.Find("Canvas/BtnSwitch/Text").GetComponent<Text>();

        btnSendText = GameObject.Find("Canvas/InputPassword/BtnSend/Text").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckEnableLogin();
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public void OnBackPress()
    {
        
    }

    /// <summary>
    /// 检查是否可点击登录
    /// </summary>
    public void CheckEnableLogin()
    {
        string name = inputName.text;
        string password = inputPwd.text;
        bool enableSubmit= (name.Length>0 && password.Length>0);
        btnSubmit.enabled = enableSubmit;
        btnSubmit.interactable = enableSubmit;
        Color nowColor;
        string color= enableSubmit ? "#FFFFFF" : "#AEAEAE";
        ColorUtility.TryParseHtmlString(color, out nowColor);
        btnSubmitText.color = nowColor;
    }

    /// <summary>
    /// 切换登录方式
    /// </summary>
    public void OnSwitchClick()
    {
        isPhonePwd = !isPhonePwd;
        btnSwitchText.text = isPhonePwd ? "切换验证码登录" : "切换密码登录";
        inputPwdHint.text = isPhonePwd ? "请输入6位数密码" : "请输入验证码";
        btnSend.gameObject.SetActive(!isPhonePwd);
        inputPwd.text = "";
        inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;
        inputPwd.characterLimit = isPhonePwd ? 6 : 4;
    }

    /// <summary>
    /// 点击发送验证码
    /// </summary>
    public void OnSendVerifyCode()
    {
        
    }

    /// <summary>
    /// 登录
    /// </summary>
    public void OnLoginSubmit()
    {
        string name = inputName.text;
        string pwd = inputPwd.text;
        if (isPhonePwd)
        {
            //用户名密码
        }
        else
        {
            //用户名验证码
        }
    }

    /// <summary>
    /// 隐私政策
    /// </summary>
    public void OnPrivacyClick()
    {

    }

    /// <summary>
    /// 用户协议
    /// </summary>
    public void OnUserAgreement()
    {

    }
}
