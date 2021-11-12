using UnityEngine;

public class PluginHelper : MonoBehaviour
{

    public void ButtonPressed()
    {
        NativeCalls.Default.sendMessageToMobileApp("fasdgasdfasdfsad");
    }

}