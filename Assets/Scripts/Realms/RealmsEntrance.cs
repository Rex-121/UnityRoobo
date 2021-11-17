using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Diagnostics;

using UniRx;

public class RealmsEntrance : MonoBehaviour
{

    [ShowInInspector]
    [LabelText("课件管理器")]
    private CoursewareManager cwManager;

    // Start is called before the first frame update
    void Start()
    {

        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        //var gb = Resources.Load("CoursewareManager") as GameObject;

        //Logging.Log("开始加载 CoursewareManager" + sw.ElapsedMilliseconds);
        //var p = Instantiate(gb);

        //Logging.Log("开始渲染 CoursewareManager" + sw.ElapsedMilliseconds);
        //sw.Stop();
        //cwManager = p.GetComponent<CoursewareManager>();

        //Observable.EveryEndOfFrame().Take(1).SelectMany(loadCW).Subscribe().AddTo(this);
    }


    //IEnumerator loadCW()
    //{
    //    Logging.Log("开始加载 CoursewareManager");
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    var gb = Resources.Load("CoursewareManager") as GameObject;

    //    Logging.Log("开始加载 CoursewareManager" + sw.ElapsedMilliseconds);
    //    var p = Instantiate(gb);

    //    Logging.Log("开始渲染 CoursewareManager" + sw.ElapsedMilliseconds);
    //    sw.Stop();
    //    cwManager = p.GetComponent<CoursewareManager>();

    //    yield return cwManager;
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
