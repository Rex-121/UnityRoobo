using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBridgeListener : MonoBehaviour
{
    public void DidReceiveFromNative(string message)
    {
        NativeBridge.Default.DidReceiveFromNative(message);
        SvuiBridge.Default.DidReceiveFromNative(message);
    }
}
