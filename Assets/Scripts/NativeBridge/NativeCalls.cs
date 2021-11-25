using System;
using UnityEngine;


[CreateAssetMenu(fileName = "原生通信", menuName = "单例SO/原生通信")]
public class NativeCalls : SingletonSO<NativeCalls>
{

    public static NativeCalls Default => Instance("原生通信");

    public void sendMessageToMobileApp(string message)
    {

#if UNITY_EDITOR
        Debug.Log("Unity Editor");
        return;
#endif

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
    public static void PushSettingMenus()
    {


#if UNITY_EDITOR
        Debug.Log("Unity Editor PushSettingMenus");
        return;
#endif

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

}


