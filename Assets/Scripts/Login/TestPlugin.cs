using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlugin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public Text log;
    string batteryData;
    string wifiData;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 140, 40), "发送通知"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("UnityCallAndroidToast", "这是Unity调用Android的Toast！");
        }

        if (GUI.Button(new Rect(10, 70, 140, 40), "求和"))
        {
          /*  AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            int sum = jo.Call<int>("Sum", new object[] { 10, 20 });
            log.text = ""+ sum;
            jo.Call("ClickShake");//调用安卓震动
        }

        if (GUI.Button(new Rect(10, 130, 140, 40), "Toast"))//创建安卓 Toast
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("CreateToast", "初始化中...");
        }

        if (GUI.Button(new Rect(10, 190, 140, 40), "立即重启应用"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication");
        }

        if (GUI.Button(new Rect(10, 250, 140, 40), "UI线程重启应用"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplicationOnUIThread");
        }

        if (GUI.Button(new Rect(10, 310, 140, 40), "重启应用"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication1");
        }

        if (GUI.Button(new Rect(10, 370, 140, 40), "5s重启应用"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication2");
        }

        if (GUI.Button(new Rect(10, 430, 140, 40), "获取安装apk"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetAllPackageName");
        }

        if (GUI.Button(new Rect(10, 490, 140, 40), "调用APP"))
        {
            /*  AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("CallThirdApp", "com.tencent.mm");
        }

        if (GUI.Button(new Rect(10, 550, 140, 40), "Unity本地推送"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("SendNotification", new string[] { "奇迹:最强者", "勇士们 魔龙讨伐即将开始" });
        }

        if (GUI.Button(new Rect(10, 610, 140, 40), "获取所有App"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetAllWidget");
        }

        if (GUI.Button(new Rect(10, 670, 140, 40), "获取已安装的App"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetInstalledPackageName");
        }

        if (GUI.Button(new Rect(10, 730, 140, 40), "获取电池信息"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            batteryData = jo.Call<string>("MonitorBatteryState");
            OnBatteryDataBack(batteryData);
        }

        if (GUI.Button(new Rect(10, 790, 140, 40), "获取wifi强度"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            wifiData = jo.Call<string>("ObtainWifiInfo");
            OnWifiDataBack(wifiData);
        }
        if (GUI.Button(new Rect(10, 850, 140, 40), "获取运营商名称"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            string simType = jo.Call<string>("CheckSIM");
            log.text = simType;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }

    }

    void GetBatteryAnWifiData()
    {
        batteryData = "";
        wifiData = "";
        
        AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
        batteryData = jo.CallStatic<string>("MonitorBatteryState");
        log.text = batteryData;
        
        wifiData = jo.CallStatic<string>("ObtainWifiInfo");
        log.text += wifiData;
        OnBatteryDataBack(batteryData);
        OnWifiDataBack(wifiData);

    }
    void OnBatteryDataBack(string batteryData)//level+"|"+scale+"|"+status;
    {
        string[] args = batteryData.Split('|');
        if (args[2] == "2")
        {
            log.text += "电池充电中";
        }
        else
        {
            log.text += "电池放电中";
        }
        float percent = int.Parse(args[0]) / float.Parse(args[1]);
        log.text += (Mathf.CeilToInt(percent) + "%").ToString();
    }
    void OnWifiDataBack(string wifiData)//strength+"|"+intToIp(ip)+"|"+mac+"|"+ssid;
    {
        //分析wifi信号强度
        //获取RSSI，RSSI就是接受信号强度指示。
        //得到的值是一个0到-100的区间值，是一个int型数据，其中0到-50表示信号最好，
        //-50到-70表示信号偏差，小于-70表示最差，
        //有可能连接不上或者掉线，一般Wifi已断则值为-200。
        log.text += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 0)
        {
            log.text += "Wifi信号强度很棒";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log.text += "Wifi信号强度一般";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log.text += "Wifi信号强度很弱";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log.text += "Wifi信号JJ了";
        }
        string ip = "IP：" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log.text += ip;
        log.text += mac;
        log.text += ssid;
    }

    /// <summary>
    /// 安卓日志
    /// </summary>
    /// <param name="str"></param>
    void OnCoderReturn(string str)
    {
        log.text += str;
    }

    void OnBatteryDataReturn(string batteryData)
    {
        string[] args = batteryData.Split('|');
        if (args[2] == "2")
        {
            log.text += "电池充电中";
        }
        else
        {
            log.text += "电池放电中";
        }
        log.text += (args[0] + "%").ToString();
    }

    void OnWifiDataReturn(string wifiData)
    {
        log.text += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 100)
        {
            log.text += "Wifi信号强度很棒";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log.text += "Wifi信号强度一般";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log.text += "Wifi信号强度很弱";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log.text += "Wifi信号JJ了";
        }
        string ip = "IP：" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log.text += ip;
        log.text += mac;
        log.text += ssid;
    }
}