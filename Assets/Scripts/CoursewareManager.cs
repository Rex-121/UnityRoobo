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

    [LabelText("课件列表")]
    public CoursewareRoundingList roundlist;


    [ShowInInspector]
    [LabelText("课件画布")]
    public GameObject cwCanvas;


    [LabelText("视频播放器")]
    public VideoCourseware videoCourseware;



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
                videoCourseware.SetRound(round);
                playlist.round = round;
                break;
        }

    }

    /// <summary>
    /// 准备开始下一课件
    /// </summary>
    void Next()
    {

        var cp = playlist.Next();

        if (cp == null)
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
                    videoCourseware.SetRound(round);
                    playlist.round = round;
                    break;
            }


            //SceneManager.LoadScene("Realm");
            return;
        }
        PlayCourseware(cp);

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
            if (!videoCourseware.Continue())
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