using UnityEngine;
using Sirenix.OdinInspector;
using System;

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

        NativeBridge.Instance.AddListenerToScreen();

        FPS.Instance.LockFrame();

        Credit.Instance.Init().gameObject.transform.SetParent(transform);

        cwCanvas.GetComponent<Canvas>().worldCamera = Camera.main;

        ClearStage();

        Next();

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

    [ReadOnly]
    [LabelText("正在播放的课件")]
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