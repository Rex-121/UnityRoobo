using UnityEngine;

public class PluginHelper : MonoBehaviour
{

    public void ButtonPressed()
    {
        NativeCalls.Shared.sendMessageToMobileApp("fasdgasdfasdfsad");
    }

}