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
    [LabelText("发送验证码文本")]
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
        bool enableSubmit = (name.Length > 0 && password.Length > 0);
        btnSubmit.enabled = enableSubmit;
        btnSubmit.interactable = enableSubmit;
        Color nowColor;
        string color = enableSubmit ? "#FFFFFF" : "#AEAEAE";
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
    /// 进入设置页
    /// </summary>
    public void EnterSetting()
    {
        //跳入设置页
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("sendUserInfo", "18810128570");
        jo.Call("sendUpdateInfo", 1);
        jo.Call("enterSettingActivity");
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


    void test()
    {
        //javaClass = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");

        //javaClass.Call("LogNativeAndroidLogcatMessage");

        //string staticMth = javaClass.CallStatic<string>("getStaticAndroidString");
        //Debug.Log("a=============staticMth=" + staticMth);

        //string a = javaClass.Call<string>("getAndroidString");

        //Debug.Log("a=============" + a);

        //string b = javaClass.Call<string>("ObtainWifiInfo");
        //Debug.Log("a=============获取wifi强度=" + b);

        ////javaClass.Call("getAndroidString");
        //javaClass.Call("UnityCallAndroidToast", "这是Unity调用Android的Toast！");

        //int sum = javaClass.Call<int>("Sum", new object[] { 10, 20 });
        //Debug.Log("a=============sum=" + sum);

        //string simType = javaClass.Call<string>("CheckSIM");
        //Debug.Log("a=============获取运营商名称=" + simType);

        //string batteryData = javaClass.Call<string>("MonitorBatteryState");
        //OnBatteryDataBack(batteryData);

        //AndroidJavaObject javaObject = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
        //string wifiData = javaObject.Call<string>("ObtainWifiInfo");
        //OnWifiDataBack(wifiData);
        /*
                javaClass.Call("GetInstalledPackageName");

                javaClass.Call("GetAllWidget");

                //Unity本地推送
                javaClass.Call("SendNotification", new string[] { "奇迹:最强者", "勇士们 魔龙讨伐即将开始" });

                //调用APP
                javaClass.Call("CallThirdApp", "com.tencent.mm");

                //获取安装apk
                javaClass.Call("GetAllPackageName");

                //5s重启应用
                javaClass.Call("RestartApplication2");

                //重启应用
                javaClass.Call("RestartApplication1");

                //UI线程重启应用
                javaClass.Call("RestartApplicationOnUIThread");

                //立即重启应用
                javaClass.Call("RestartApplication");

                //创建安卓 Toast
                javaClass.Call("CreateToast", "初始化中...");

                //调用安卓震动
                javaClass.Call("ClickShake");

                javaClass.Call("UnityCallAndroidToast", "这是Unity调用Android的Toast！");*/
    }

    /// <summary>
    /// 电量信息
    /// </summary>
    public void GetBatteryLevel()
    {
        try
        {
            //获取电量
            float batteryLevel = SystemInfo.batteryLevel * 100;
            Debug.Log("a=============电量：" + batteryLevel);
            BatteryStatus batteryStatus = SystemInfo.batteryStatus;
            Debug.Log("a=============充电状态：" + batteryStatus);
            inputName.text = "" + batteryStatus;

        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
    }

    /// <summary>
    /// Wifi信息
    /// </summary>
    public void GetWifiInfo()
    {
        //获取Wifi信息
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
            Debug.Log("a=============OnWifiDataBack=WIFI 未连接");
            return;
        }
        WifiJson wifi = JsonUtility.FromJson<WifiJson>(wifiData);
        //分析wifi信号强度
        //获取RSSI，RSSI就是接受信号强度指示。
        //得到的值是一个0到-100的区间值，是一个int型数据，其中0到-50表示信号最好，
        //-50到-70表示信号偏差，小于-70表示最差，
        //有可能连接不上或者掉线，一般Wifi已断则值为-200。
        
        log += wifiData + "\n";
        log += "WIFI名："+wifi.ssid + "\n";
        log +="信号强度："+ wifi.strength + "\n";
        int strength = wifi.strength;
        if (strength > -50 && strength < 0)
        {
            log += "Wifi信号强度很棒";
        }
        else if (strength > -70 && strength < -50)
        {
            log += "Wifi信号强度一般";
        }
        else if (strength > -150 && strength < -70)
        {
            log += "Wifi信号强度很弱";
        }
        else if (strength <= -200)
        {
            log += "Wifi信号JJ了";
        }
        Debug.Log("a=============OnWifiDataBack=" + log);
    }
}
