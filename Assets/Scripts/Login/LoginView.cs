using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;
using System;

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

    [ShowInInspector]
    [LabelText("������֤���ı�")]
    private Text btnSendText;
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

        // btnSendText = GameObject.Find("Canvas/InputPassword/BtnSend/Text").GetComponent<Text>();
        test();
        GetBatteryLevel();

        GetWifiInfo();


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
        bool enableSubmit = (name.Length > 0 && password.Length > 0);
        btnSubmit.enabled = enableSubmit;
        btnSubmit.interactable = enableSubmit;
        Color nowColor;
        string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";
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
    /// ��������ҳ
    /// </summary>
    public void EnterSetting()
    {
        //��������ҳ
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("sendUserInfo", "18810128570");
        jo.Call("sendUpdateInfo", 1);
        jo.Call("enterSettingActivity");
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


    void test()
    {
        //javaClass = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");

        //javaClass.Call("LogNativeAndroidLogcatMessage");

        //string staticMth = javaClass.CallStatic<string>("getStaticAndroidString");
        //Debug.Log("a=============staticMth=" + staticMth);

        //string a = javaClass.Call<string>("getAndroidString");

        //Debug.Log("a=============" + a);

        //string b = javaClass.Call<string>("ObtainWifiInfo");
        //Debug.Log("a=============��ȡwifiǿ��=" + b);

        ////javaClass.Call("getAndroidString");
        //javaClass.Call("UnityCallAndroidToast", "����Unity����Android��Toast��");

        //int sum = javaClass.Call<int>("Sum", new object[] { 10, 20 });
        //Debug.Log("a=============sum=" + sum);

        //string simType = javaClass.Call<string>("CheckSIM");
        //Debug.Log("a=============��ȡ��Ӫ������=" + simType);

        //string batteryData = javaClass.Call<string>("MonitorBatteryState");
        //OnBatteryDataBack(batteryData);

        //AndroidJavaObject javaObject = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
        //string wifiData = javaObject.Call<string>("ObtainWifiInfo");
        //OnWifiDataBack(wifiData);
        /*
                javaClass.Call("GetInstalledPackageName");

                javaClass.Call("GetAllWidget");

                //Unity��������
                javaClass.Call("SendNotification", new string[] { "�漣:��ǿ��", "��ʿ�� ħ���ַ�������ʼ" });

                //����APP
                javaClass.Call("CallThirdApp", "com.tencent.mm");

                //��ȡ��װapk
                javaClass.Call("GetAllPackageName");

                //5s����Ӧ��
                javaClass.Call("RestartApplication2");

                //����Ӧ��
                javaClass.Call("RestartApplication1");

                //UI�߳�����Ӧ��
                javaClass.Call("RestartApplicationOnUIThread");

                //��������Ӧ��
                javaClass.Call("RestartApplication");

                //������׿ Toast
                javaClass.Call("CreateToast", "��ʼ����...");

                //���ð�׿��
                javaClass.Call("ClickShake");

                javaClass.Call("UnityCallAndroidToast", "����Unity����Android��Toast��");*/
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    public void GetBatteryLevel()
    {
        try
        {
            //��ȡ����
            float batteryLevel = SystemInfo.batteryLevel * 100;
            Debug.Log("a=============������" + batteryLevel);
            BatteryStatus batteryStatus = SystemInfo.batteryStatus;
            Debug.Log("a=============���״̬��" + batteryStatus);
            inputName.text = "" + batteryStatus;

        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
    }

    /// <summary>
    /// Wifi��Ϣ
    /// </summary>
    public void GetWifiInfo()
    {
        //��ȡWifi��Ϣ
        AndroidJavaObject javaObject = new AndroidJavaObject("com.pudding.settingplugins.SettingPlugins");
        string wifiData = javaObject.Call<string>("obtainWifiInfo");
        OnWifiDataBack(wifiData);
    }

    [System.Serializable]
    class WifiJson {
        public int strength;
        public string ssid;
    }


    void OnWifiDataBack(string wifiData)//strength|ssid;
    {
        string log = "";

        if (wifiData.Length==0)
        {
            Debug.Log("a=============OnWifiDataBack=WIFI δ����");
            return;
        }
        WifiJson wifi = JsonUtility.FromJson<WifiJson>(wifiData);
        //����wifi�ź�ǿ��
        //��ȡRSSI��RSSI���ǽ����ź�ǿ��ָʾ��
        //�õ���ֵ��һ��0��-100������ֵ����һ��int�����ݣ�����0��-50��ʾ�ź���ã�
        //-50��-70��ʾ�ź�ƫ�С��-70��ʾ��
        //�п������Ӳ��ϻ��ߵ��ߣ�һ��Wifi�Ѷ���ֵΪ-200��
        
        log += wifiData + "\n";
        log += "WIFI����"+wifi.ssid + "\n";
        log +="�ź�ǿ�ȣ�"+ wifi.strength + "\n";
        int strength = wifi.strength;
        if (strength > -50 && strength < 0)
        {
            log += "Wifi�ź�ǿ�Ⱥܰ�";
        }
        else if (strength > -70 && strength < -50)
        {
            log += "Wifi�ź�ǿ��һ��";
        }
        else if (strength > -150 && strength < -70)
        {
            log += "Wifi�ź�ǿ�Ⱥ���";
        }
        else if (strength <= -200)
        {
            log += "Wifi�ź�JJ��";
        }
        Debug.Log("a=============OnWifiDataBack=" + log);
    }
}
