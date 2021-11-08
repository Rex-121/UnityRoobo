using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Svui", menuName = "单例SO/Svui")]
public class Svui : SingletonSO<Svui>
{

    public void AddListenerToScreen()
    {
        var gb = new GameObject("SvuiListener");
        gb.AddComponent<SvuiListener>();


        
    }


    public void SendSvuiData(string data)
    {
        var a = new SvuiData(new FollowReadData());

        var mes = JsonUtility.ToJson(a);

        Debug.Log(mes);

        NativeCalls.Instance.sendMessageToMobileApp(mes);
    }


    public void SvuiMessageDeliver(string message) {

        Debug.Log("Svui 数据回归");

        Debug.Log(message);

    }


}
