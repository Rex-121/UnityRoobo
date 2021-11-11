using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBridgeListener : MonoBehaviour
{
    public void DidEndSvui(string message)
    {
        NativeBridge.Instance.DidReceiveFromNative(message);
    }

}
