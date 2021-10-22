using System.Runtime.InteropServices;

#if UNITY_IOS
public class NativeAPI
{
    [DllImport("__Internal")]
    public static extern void sendMessageToMobileApp(string message);
}
#endif
