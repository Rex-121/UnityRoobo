using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CW_FollowRead : CoursewarePlayer
{


    [SerializeField]
    Transform readTextPosition;

    [SerializeField]
    GameObject canvas;

    void Start()
    {

        //var f = "{ \"audio\": \"https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-07-13/c3ml5t0rjdcmt7uaegqg.mp3\",\"text\": \"ok\", \"textTimeLine\": [{ \"from\": 1260, \"to\": 1350, \"word\": \"ok\"}]}";
        //Delay.Instance.DelayToCall(4, () =>
        //{
        //    NativeCalls.Instance.sendMessageToMobileApp(f);
        //});


        var d = Camera.main.WorldToScreenPoint(readTextPosition.position);
        Logging.Log(d);


        canvas = Instantiate(canvas, Vector3.zero, Quaternion.identity, cwCanvas.transform);

        canvas.transform.localScale = Vector3.one;

        Delay.Instance.DelayToCall(11, () =>
        {

            Destroy(canvas);

            DidEndCourseware(this);
        });

    }
}
