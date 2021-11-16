using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Collections;

using System.Diagnostics;

[RequireComponent(typeof(CoursewareCredit))]
public class CoursewareManager : MonoBehaviour
{

    [LabelText("课件列表")]
    public CoursewarePlaylist playlist;


    [ShowInInspector]
    [LabelText("课件画布")]
    public GameObject cwCanvas;

    private void Start()
    {
        cwCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void EveryThingReady(Unit a)
    {
        ClearStage();

        Next();
    }

    void OnEnable()
    {
        //.SelectMany(Observable.FromCoroutine(DrawCreditBlockCanvas))
        //.SelectMany(Observable.FromCoroutine(AddListenerToScreen))

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Observable.EveryEndOfFrame().Take(1)
            .SelectMany(Observable.FromCoroutine(AddListenerToScreen))
            .SelectMany(Observable.FromCoroutine(DrawFPS))
            .Select(v =>
            {
                sw.Stop();
                Logging.Log("加载完成场景所需" + sw.ElapsedMilliseconds);
                return v;
            })
            .Subscribe(EveryThingReady);


    }

    IEnumerator DrawCreditBlockCanvas()
    {
        var a = Credit.Default.Init().gameObject;
        a.transform.SetParent(transform);
        yield return 0;
    }

    IEnumerator AddListenerToScreen()
    {
        NativeBridge.Default.AddListenerToScreen();
        yield return 0;
    }

    IEnumerator DrawFPS()
    {
        FPS.Default.LockFrame();
        yield return 0;
    }

    /// <summary>
    /// 准备开始下一课件
    /// </summary>
    void Next()
    {

        var cp = playlist.Next();
        if (cp == null) return;

        Stopwatch sw = new Stopwatch();
        sw.Start();

        //var r = Resources.Load("Prefabs/Courseware/FollowRead/FollowRead") as GameObject;
        //Logging.Log("读取资源" + sw.ElapsedMilliseconds);


        playingCW = Instantiate(cp.coursewarePlayer);
        sw.Stop();
        Logging.Log("生产资源" + sw.ElapsedMilliseconds);



        //Stopwatch sws = new Stopwatch();
        //sws.Start();

        //var rf = Resources.Load("Prefabs/Courseware/PictureMatch/PictureMatch") as GameObject;
        //Logging.Log("读取资源" + sw.ElapsedMilliseconds);
        ////if (cp == null) return;
        ////playingCW = Instantiate(rf);
        //sws.Stop();
        //Logging.Log("生产资源" + sw.ElapsedMilliseconds);


        /// 初始化数据
        //cp.MakeData(playingCW, "");

        playingCW.transform.parent = transform;

        CoursewarePlayer player = playingCW.GetComponent<CoursewarePlayer>();

        if (player == null) Next();

        player.cwCanvas = cwCanvas;
        player.lifetimeDelegate = new CoursewareLifetimeListener((c) => { }, (c) => { }, (c) => { }, end: DidEndACourseware);

        /// 绑定得分事件
        player.creditDelegate = GetComponent<CoursewareCredit>();


        player.Play();
    }

    //[ReadOnly]
    //[LabelText("正在播放的课件")]
    public GameObject playingCW;

    /// <summary>
    /// 清空舞台
    /// </summary>
    public void ClearStage()
    {

        if (playingCW == null) return;

        Destroy(playingCW);
    }

    /// <summary>
    /// 课件播放结束
    /// </summary>
    /// <param name="player">课件</param>
    void DidEndACourseware(CoursewarePlayer player)
    {
        ClearStage();
        Logging.Log(player + " End");
        Next();
    }
}