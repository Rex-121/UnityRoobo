using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBridgeListener : MonoBehaviour
{
    public void DidReceiveFromNative(string message)
    {

        NativeBridge.Shared.DidReceiveFromNative(message);
        SvuiBridge.Shared.DidReceiveFromNative(message);
    }
}
