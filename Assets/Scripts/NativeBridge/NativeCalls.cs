using System;
using UnityEngine;


[CreateAssetMenu(fileName = "原生通信", menuName = "单例SO/原生通信")]
public class NativeCalls : SingletonSO<NativeCalls>
{


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
            overrideActivity.Call("oralEvaluate", "id",message);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
#endif

    }

}


