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
    /// ���ذ�ť
    /// </summary>
    public void OnBackPress()
    {
        
    }

    /// <summary>
    /// ����Ƿ�ɵ����¼
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
    /// �л���¼��ʽ
    /// </summary>
    public void OnSwitchClick()
    {
        isPhonePwd = !isPhonePwd;
        btnSwitchText.text = isPhonePwd ? "�л���֤���¼" : "�л������¼";
        inputPwdHint.text = isPhonePwd ? "������6λ������" : "��������֤��";
        btnSend.gameObject.SetActive(!isPhonePwd);
        inputPwd.text = "";
        inputPwd.contentType = isPhonePwd ? InputField.ContentType.Standard : InputField.ContentType.IntegerNumber;
        inputPwd.characterLimit = isPhonePwd ? 6 : 4;
    }

    /// <summary>
    /// ���������֤��
    /// </summary>
    public void OnSendVerifyCode()
    {
        
    }

    /// <summary>
    /// ��¼
    /// </summary>
    public void OnLoginSubmit()
    {
        string name = inputName.text;
        string pwd = inputPwd.text;
        if (isPhonePwd)
        {
            //�û�������
        }
        else
        {
            //�û�����֤��
        }
    }

    /// <summary>
    /// ��˽����
    /// </summary>
    public void OnPrivacyClick()
    {

    }

    /// <summary>
    /// �û�Э��
    /// </summary>
    public void OnUserAgreement()
    {

    }
}
