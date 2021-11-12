using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Collections;

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
        EveryThingReady(Unit.Default);
        //Observable.EveryEndOfFrame().Take(1)
        //    .SelectMany(Observable.FromCoroutine(DrawCreditBlockCanvas))
        //    .SelectMany(Observable.FromCoroutine(AddListenerToScreen))
        //    .SelectMany(Observable.FromCoroutine(DrawFPS))
        //    .Subscribe(EveryThingReady);
    }

    IEnumerator DrawCreditBlockCanvas()
    {
        var a = Credit.Instance.Init().gameObject;
        a.transform.SetParent(transform);
        yield return 0;
    }

    IEnumerator AddListenerToScreen()
    {
        NativeBridge.Instance.AddListenerToScreen();
        yield return 0;
    }

    IEnumerator DrawFPS()
    {
        FPS.Instance.LockFrame();
        yield return 0;
    }

    /// <summary>
    /// 准备开始下一课件
    /// </summary>
    void Next()
    {

        var cp = playlist.Next();

        if (cp == null) return;

        playingCW = Instantiate(cp.coursewarePlayer);

        /// 初始化数据
        cp.MakeData(playingCW, "");

        playingCW.transform.parent = transform;
        playingCW.GetComponent<CoursewarePlayer>().cwCanvas = cwCanvas;
        playingCW.GetComponent<CoursewarePlayer>().lifetimeDelegate = new CoursewareLifetimeListener((c) => { }, (c) => { }, (c) => { }, end: DidEndACourseware);

        /// 绑定得分事件
        playingCW.GetComponent<CoursewarePlayer>().creditDelegate = GetComponent<CoursewareCredit>();


        playingCW.GetComponent<CoursewarePlayer>().Play();
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