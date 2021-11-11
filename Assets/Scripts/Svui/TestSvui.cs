using UnityEngine;

public class TestSvui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var f = "{ \"audio\": \"https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-07-13/c3ml5t0rjdcmt7uaegqg.mp3\",\"text\": \"ok\", \"textTimeLine\": [{ \"from\": 1260, \"to\": 1350, \"word\": \"ok\"}]}";
        Delay.Instance.DelayToCall(2, () =>
        {
            NativeBridge.Instance.SendDataToNative(f);
        });
    }
}
