using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Diagnostics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CoursewareCredit))]
public class CoursewareManager : MonoBehaviour
{

    [LabelText("课件列表")]
    public CoursewarePlaylist playlist;


    [ShowInInspector]
    [LabelText("课件画布")]
    public GameObject cwCanvas;


    void EveryThingReady()
    {
        ClearStage();

        Next();
    }

    public void Play()
    {
        EveryThingReady();
    }

    void Start()
    {
        Observable.EveryEndOfFrame().Take(1)   
            .Subscribe().AddTo(this);
    }

    /// <summary>
    /// 准备开始下一课件
    /// </summary>
    void Next()
    {

        var cp = playlist.Next();
        if (cp == null)
        {
            SceneManager.LoadScene("Realm");
            return;
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();

        playingCW = Instantiate(cp.coursewarePlayer);
        sw.Stop();
        Logging.Log("生产资源" + sw.ElapsedMilliseconds);

        /// 初始化数据
        cp.MakeData(playingCW);

        playingCW.transform.parent = transform;

        CoursewarePlayer player = playingCW.GetComponent<CoursewarePlayer>();

        if (player == null) Next();

        player.cwCanvas = cwCanvas;
        player.lifetimeDelegate = new CoursewareLifetimeListener((c) => { }, (c) => { }, (c) => { }, end: DidEndACourseware);

        /// 绑定得分事件
        player.creditDelegate = GetComponent<CoursewareCredit>();


        player.Play();
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