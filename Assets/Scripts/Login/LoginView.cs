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

           GetBatteryAnWifiData();
  
    }

    private AndroidJavaObject javaClass;
    void test()
    {
        javaClass = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");

        javaClass.Call("LogNativeAndroidLogcatMessage");

        string staticMth = javaClass .CallStatic<string>("getStaticAndroidString");
        Debug.Log("a=============staticMth=" + staticMth);

        string a = javaClass.Call<string>("getAndroidString");

        Debug.Log("a=============" + a);

        string b = javaClass.Call<string>("ObtainWifiInfo");
        Debug.Log("a=============��ȡwifiǿ��=" + b);

        //javaClass.Call("getAndroidString");
        javaClass.Call("UnityCallAndroidToast", "����Unity����Android��Toast��");

        int sum = javaClass.Call<int>("Sum", new object[] { 10, 20 });
        Debug.Log("a=============sum=" + sum);

        string simType = javaClass.Call<string>("CheckSIM");
        Debug.Log("a=============��ȡ��Ӫ������=" + simType);

        string batteryData = javaClass.Call<string>("MonitorBatteryState");
        OnBatteryDataBack(batteryData);

        string wifiData = javaClass.Call<string>("ObtainWifiInfo");
        OnWifiDataBack(wifiData);
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

    public void GetBatteryLevel()
    {
        try
        {
            //��ȡ����
            float batteryLevel= SystemInfo.batteryLevel* 100;
            Debug.Log("a=============������" + batteryLevel);
            BatteryStatus batteryStatus = SystemInfo.batteryStatus ;
            Debug.Log("a=============���״̬��" + batteryStatus);
            inputName.text = "" + batteryStatus;
            
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
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
        //��������ҳ
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("enterSettingActivity");
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


    string batteryData;
    string wifiData;

    void GetBatteryAnWifiData()
    {
        
        batteryData = "";
        wifiData = "";
        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //batteryData = jo.Call<string>("MonitorBatteryState");
       
        //Debug.Log("a=============batteryData" + batteryData);
        //AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //AndroidJavaObject jo1 = jc1.GetStatic<AndroidJavaObject>("currentActivity");
        //wifiData = jo1.Call<string>("ObtainWifiInfo");
        
        Debug.Log("a=============wifiData" + wifiData);
        // OnBatteryDataBack(batteryData);
        OnWifiDataBack(wifiData);


    }
    void OnBatteryDataBack(string batteryData)//level+"|"+scale+"|"+status;
    {
        string[] args = batteryData.Split('|');
        if (args[2] == "2")
        {
           
            Debug.Log("a=============��س����" );
        }
        else
        {
            Debug.Log("a=============��طŵ���");
        }
        float percent = int.Parse(args[0]) / float.Parse(args[1]);
        string tx= (Mathf.CeilToInt(percent) + "%").ToString();
        Debug.Log("a=============���="+tx);
    }
    void OnWifiDataBack(string wifiData)//strength+"|"+intToIp(ip)+"|"+mac+"|"+ssid;
    {
        //����wifi�ź�ǿ��
        //��ȡRSSI��RSSI���ǽ����ź�ǿ��ָʾ��
        //�õ���ֵ��һ��0��-100������ֵ����һ��int�����ݣ�����0��-50��ʾ�ź���ã�
        //-50��-70��ʾ�ź�ƫ�С��-70��ʾ��
        //�п������Ӳ��ϻ��ߵ��ߣ�һ��Wifi�Ѷ���ֵΪ-200��
        string log = "";
        log += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 0)
        {
            log += "Wifi�ź�ǿ�Ⱥܰ�";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log += "Wifi�ź�ǿ��һ��";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log += "Wifi�ź�ǿ�Ⱥ���";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log += "Wifi�ź�JJ��";
        }
        string ip = "IP��" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log += ip;
        log += mac;
        log += ssid;
        Debug.Log("a=============OnWifiDataBack=" + log);
    }

    /// <summary>
    /// ��׿��־
    /// </summary>
    /// <param name="str"></param>
    void OnCoderReturn(string str)
    {

        Debug.Log("a=============OnCoderReturn=" + str);
    }

    void OnBatteryDataReturn(string batteryData)
    {
        string[] args = batteryData.Split('|');
        if (args[2] == "2")
        {
            Debug.Log("a=============��س����=" );
        }
        else
        {
            Debug.Log("a=============��طŵ���=");
        }
        string text = (args[0] + "%").ToString();
        Debug.Log("a=============��طŵ���="+ text);
    }

    void OnWifiDataReturn(string wifiData)
    {
        string log = "";
        log += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 100)
        {
            log += "Wifi�ź�ǿ�Ⱥܰ�";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log += "Wifi�ź�ǿ��һ��";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log += "Wifi�ź�ǿ�Ⱥ���";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log += "Wifi�ź�JJ��";
        }
        string ip = "IP��" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log += ip;
        log += mac;
        log += ssid;
        Debug.Log("a=============OnWifiDataReturn=" + log);
    }
}
