using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NativeBridge", menuName = "单例SO/NativeBridge")]
public class NativeBridge : SingletonSO<NativeBridge>
{

    // 将监听器加入场景
    public void AddListenerToScreen()
    {
        var gb = new GameObject("NativeBridgeListener");
        gb.AddComponent<NativeBridgeListener>();
    }

    /// <summary>
    /// 发送信息至原生平台
    /// </summary>
    /// <param name="data">JSON字符串</param>
    public void SendDataToNative(string data)
    {
        var a = new SvuiData(new FollowReadData());

        var mes = JsonUtility.ToJson(a);

        Debug.Log(mes);

        NativeCalls.Instance.sendMessageToMobileApp(mes);
    }


    /// <summary>
    /// 原生端发送信息至Unity，需要有 `NativeBridgeListener`
    /// </summary>
    /// <param name="message">JSON字符串</param>
    public void DidReceiveFromNative(string message) {

        Debug.Log("Svui 数据回归");

        Debug.Log(message);

    }


}
