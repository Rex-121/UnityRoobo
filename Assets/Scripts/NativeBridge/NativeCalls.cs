using UnityEngine;

[CreateAssetMenu(fileName = "原生通信", menuName = "单例SO/原生通信")]
public class NativeCalls : SingletonSO<NativeCalls>
{


    public void sendMessageToMobileApp(string message)
    {
#if UNITY_ANDROID

#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.sendMessageToMobileApp(message);
#endif
    }

}


