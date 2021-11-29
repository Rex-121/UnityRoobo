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
        if (GUI.Button(new Rect(10, 10, 140, 40), "����֪ͨ"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("UnityCallAndroidToast", "����Unity����Android��Toast��");
        }

        if (GUI.Button(new Rect(10, 70, 140, 40), "���"))
        {
          /*  AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            int sum = jo.Call<int>("Sum", new object[] { 10, 20 });
            log.text = ""+ sum;
            jo.Call("ClickShake");//���ð�׿��
        }

        if (GUI.Button(new Rect(10, 130, 140, 40), "Toast"))//������׿ Toast
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("CreateToast", "��ʼ����...");
        }

        if (GUI.Button(new Rect(10, 190, 140, 40), "��������Ӧ��"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication");
        }

        if (GUI.Button(new Rect(10, 250, 140, 40), "UI�߳�����Ӧ��"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplicationOnUIThread");
        }

        if (GUI.Button(new Rect(10, 310, 140, 40), "����Ӧ��"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication1");
        }

        if (GUI.Button(new Rect(10, 370, 140, 40), "5s����Ӧ��"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("RestartApplication2");
        }

        if (GUI.Button(new Rect(10, 430, 140, 40), "��ȡ��װapk"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetAllPackageName");
        }

        if (GUI.Button(new Rect(10, 490, 140, 40), "����APP"))
        {
            /*  AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
              AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("CallThirdApp", "com.tencent.mm");
        }

        if (GUI.Button(new Rect(10, 550, 140, 40), "Unity��������"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("SendNotification", new string[] { "�漣:��ǿ��", "��ʿ�� ħ���ַ�������ʼ" });
        }

        if (GUI.Button(new Rect(10, 610, 140, 40), "��ȡ����App"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetAllWidget");
        }

        if (GUI.Button(new Rect(10, 670, 140, 40), "��ȡ�Ѱ�װ��App"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            jo.Call("GetInstalledPackageName");
        }

        if (GUI.Button(new Rect(10, 730, 140, 40), "��ȡ�����Ϣ"))
        {
            /* AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
             AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            batteryData = jo.Call<string>("MonitorBatteryState");
            OnBatteryDataBack(batteryData);
        }

        if (GUI.Button(new Rect(10, 790, 140, 40), "��ȡwifiǿ��"))
        {
            /*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");*/
            AndroidJavaObject jo = new AndroidJavaObject("com.pudding.settingplugins.SettingClass");
            wifiData = jo.Call<string>("ObtainWifiInfo");
            OnWifiDataBack(wifiData);
        }
        if (GUI.Button(new Rect(10, 850, 140, 40), "��ȡ��Ӫ������"))
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
            log.text += "��س����";
        }
        else
        {
            log.text += "��طŵ���";
        }
        float percent = int.Parse(args[0]) / float.Parse(args[1]);
        log.text += (Mathf.CeilToInt(percent) + "%").ToString();
    }
    void OnWifiDataBack(string wifiData)//strength+"|"+intToIp(ip)+"|"+mac+"|"+ssid;
    {
        //����wifi�ź�ǿ��
        //��ȡRSSI��RSSI���ǽ����ź�ǿ��ָʾ��
        //�õ���ֵ��һ��0��-100������ֵ����һ��int�����ݣ�����0��-50��ʾ�ź���ã�
        //-50��-70��ʾ�ź�ƫ�С��-70��ʾ��
        //�п������Ӳ��ϻ��ߵ��ߣ�һ��Wifi�Ѷ���ֵΪ-200��
        log.text += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 0)
        {
            log.text += "Wifi�ź�ǿ�Ⱥܰ�";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log.text += "Wifi�ź�ǿ��һ��";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log.text += "Wifi�ź�ǿ�Ⱥ���";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log.text += "Wifi�ź�JJ��";
        }
        string ip = "IP��" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log.text += ip;
        log.text += mac;
        log.text += ssid;
    }

    /// <summary>
    /// ��׿��־
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
            log.text += "��س����";
        }
        else
        {
            log.text += "��طŵ���";
        }
        log.text += (args[0] + "%").ToString();
    }

    void OnWifiDataReturn(string wifiData)
    {
        log.text += wifiData;
        string[] args = wifiData.Split('|');
        if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 100)
        {
            log.text += "Wifi�ź�ǿ�Ⱥܰ�";
        }
        else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
        {
            log.text += "Wifi�ź�ǿ��һ��";
        }
        else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
        {
            log.text += "Wifi�ź�ǿ�Ⱥ���";
        }
        else if (int.Parse(args[0]) < -200)
        {
            log.text += "Wifi�ź�JJ��";
        }
        string ip = "IP��" + args[1];
        string mac = "MAC:" + args[2];
        string ssid = "Wifi Name:" + args[3];
        log.text += ip;
        log.text += mac;
        log.text += ssid;
    }
}