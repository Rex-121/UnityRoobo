using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class CW_FollowRead : CoursewarePlayer
{


    [SerializeField]
    GameObject canvas;


    void Start()
    {
        
        var s = UniRx.Observable.Timer(TimeSpan.FromSeconds(11)).TakeUntilDestroy(this);

        var a = s.Subscribe(_ =>
       {
           Logging.Log("fasdfas");
       });


        //MyButton.onClick.AsObservable().Subscribe(_ => enemy.CurrentHp.Value -= 99);

        //var f = "{ \"audio\": \"https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-07-13/c3ml5t0rjdcmt7uaegqg.mp3\",\"text\": \"ok\", \"textTimeLine\": [{ \"from\": 1260, \"to\": 1350, \"word\": \"ok\"}]}";
        //Delay.Instance.DelayToCall(4, () =>
        //{
        //    NativeCalls.Instance.sendMessageToMobileApp(f);
        //});

        canvas = Instantiate(canvas, Vector3.zero, Quaternion.identity, cwCanvas.transform);

        canvas.transform.localScale = Vector3.one;


        string[] values = { "Ha" , "Hunter", "Tom", "Lily", "Jay", "Jim", "Kuku", "Locu" };

        canvas.GetComponent<CW_FollowRead_Canvas>().MakeData(new List<string>(values));


        Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ =>
        {
            Destroy(canvas);


            creditDelegate.PlayCreditOnScreen(new Score(), () =>
            {
                DidEndCourseware(this);
            });

        }).AddTo(this);


    }

    private void OnDestroy()
    {
        if (canvas != null)
        {
            Destroy(canvas);
        }
    }
}
