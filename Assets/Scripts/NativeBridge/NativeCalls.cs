using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "原生通信", menuName = "单例SO/原生通信")]
public class NativeCalls : SingletonSO<NativeCalls>
{

    public void sendMessageToMobileApp(string message)
    {


#if UNITY_IOS
        NativeAPI.sendMessageToMobileApp(message);
#elif UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.roobo.aiclasslibrary.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            string[] p = { "id", message };
            overrideActivity.Call("oralEvaluate", "id", message);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
#endif

    }


    /// <summary>
    ///  打开设置按钮
    /// </summary>
    public void PushSettingMenus()
    {
        Debug.Log("PushSettingMenus");

#if UNITY_IOS
        Debug.Log("iOS PushSettingMenus");
#elif UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("enterSettingActivity");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
#endif


    }

    [Serializable]
    public struct Wifi: IEqualityComparer<NativeCalls.Wifi>
    {

        public int strength;

        public string ssid;

        public Wifi(int s, string n)
        {
            strength = s;
            ssid = "";
        }

        public static Wifi None()
        {
            return new Wifi(0, "");
        }

        public bool Equals(Wifi x, Wifi y)
        {
            return x.ssid == y.ssid && x.strength == y.strength;
        }

        public int GetHashCode(Wifi obj)
        {
            return obj.GetHashCode();
        }
    }


    /// <summary>
    ///  打开设置按钮
    /// </summary>
    public Wifi WifiStrength()
    {
        return Wifi.None();
#if UNITY_IOS
        return Wifi.None();
#elif UNITY_ANDROID
        try
        {
            //获取Wifi信息
            AndroidJavaObject javaObject = new AndroidJavaObject("com.pudding.settingplugins.SettingPlugins");
            string wifiData = javaObject.Call<string>("obtainWifiInfo");
            //OnWifiDataBack(wifiData);
            return JsonUtility.FromJson<Wifi>(wifiData);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return Wifi.None();
        }
#endif

    }

}


