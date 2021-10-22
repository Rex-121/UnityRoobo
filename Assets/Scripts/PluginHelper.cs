using UnityEngine;

public class PluginHelper : MonoBehaviour
{

    public void ButtonPressed()
    {
        NativeCalls.Instance.sendMessageToMobileApp("fasdgasdfasdfsad");
    }

}