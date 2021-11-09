using UnityEngine;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(CoursewareCredit))]
public class CoursewareManager : MonoBehaviour
{

    [LabelText("课件列表")]
    public CoursewarePlaylist playlist;

    private void Start()
    {

        Svui.Instance.AddListenerToScreen();

        FPS.Instance.LockFrame();



        CWCanvas.Instance.Init(Camera.main).gameObject.transform.SetParent(transform);

        Credit.Instance.Init().gameObject.transform.SetParent(transform);


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

        /// 绑定结束事件
        //playingCW.GetComponent<CoursewarePlayer>().DidEndThisCourseware += DidEndACourseware;


        playingCW.GetComponent<CoursewarePlayer>().lifetimeDelegate = new CoursewareLifetimeListener((c) => { }, (c) => { }, (c) => { }, end: DidEndACourseware);
        /// 绑定得分事件
        playingCW.GetComponent<CoursewarePlayer>().creditDelegate = GetComponent<CoursewareCredit>();
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