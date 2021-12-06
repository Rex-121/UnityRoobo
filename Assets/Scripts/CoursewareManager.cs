using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Diagnostics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CoursewareCredit), typeof(CoursewarePlaylist), typeof(CoursewareRoundingList))]
[RequireComponent(typeof(CoursewareVideoPlaylist))]
public class CoursewareManager : MonoBehaviour
{

    [HideInInspector]
    public CoursewarePlaylist playlist;

    [HideInInspector]
    public CoursewareRoundingList roundlist;


    [ShowInInspector]
    [LabelText("课件画布")]
    public GameObject cwCanvas;


    [LabelText("视频播放器")]
    public CoursewareVideoPlaylist videoPlaylist;



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


        FPS.Shared.LockFrame();

        API.GetCoursePlayInfo()
            .Subscribe(GetAllRoundList, (e) =>
        {
            Logging.Log((e as HttpError).message);
        }).AddTo(this);


        Observable.EveryEndOfFrame().Take(1)
            .Subscribe().AddTo(this);
    }


    void GetAllRoundList(RoundQueue queue)
    {
        foreach (var r in queue.rounds)
        {
            GetComponent<CoursewareRoundingList>().SetRoundList(r.playlist);
        }
        Play();
    }


    public void NextRound()
    {

        var round = roundlist.GetRound();

        if (round == null)
        {
            SceneManager.LoadScene("Realm");
            return;
        }

        switch (round.type)
        {
            case RoundIsPlaying.Type.picture:
                playlist.round = round;
                Next();
                break;
            case RoundIsPlaying.Type.video:
                videoPlaylist.round = round;
                break;
        }

    }

    /// <summary>
    /// 准备开始下一课件
    /// </summary>
    void Next()
    {

        var cp = playlist.Next();

        /// 如果还有课件，直接播放
        if (cp != null) goto play;

        /// 重新分配round
        var round = roundlist.GetRound();

        /// 如果没有round，返回首页
        if (round == null) goto exit;


        //playlist.round = round;


        switch (round.type)
        {
            case RoundIsPlaying.Type.picture:
                playlist.round = round;
                goto remake;

            case RoundIsPlaying.Type.video:
                videoPlaylist.round = round;
                //playlist.round = round;
                return;
        }


    remake:
        Next();
        return;

    exit:
        Exit();
        return;

    play:
        PlayCourseware(cp);
        return;
    }

    /// <summary>
    /// 退出
    /// </summary>
    void Exit()
    {
        SceneManager.LoadScene("Realm");
    }

    public void PlayCourseware(CoursewarePlayer_SO cp)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        playingCW = Instantiate(cp.coursewarePlayer, transform);

        sw.Stop();
        Logging.Log("生产资源" + sw.ElapsedMilliseconds);

        /// 初始化数据
        cp.MakeData(playingCW);

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




        if (roundlist.currentRound.type == RoundIsPlaying.Type.video)
        {
            if (!videoPlaylist.Continue())
            {
                Next();
            }
        }
        else
        {
            Next();
        }


    }
}