using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Linq;
using UniRx;

[RequireComponent(typeof(CoursewareCredit), typeof(CoursewarePlaylist), typeof(CoursewareRoundingList))]
[RequireComponent(typeof(CoursewareVideoPlaylist))]
public class CoursewareManager : MonoBehaviour
{
    [HideInInspector]
    public CoursewareRoundingList rounding;

    [HideInInspector]
    public CoursewarePlaylist playlist;

    [HideInInspector]
    public CoursewareRoundingList roundlist;


    [ShowInInspector]
    [LabelText("课件画布")]
    public GameObject cwCanvas;


    [LabelText("视频播放器")]
    public CoursewareVideoPlaylist videoPlaylist;

    private void Awake()
    {
        rounding = GetComponent<CoursewareRoundingList>();
    }

    void EveryThingReady()
    {
        ClearStage();
    }

    public void Play()
    {
        EveryThingReady();
    }

    void Start()
    {


        FPS.Shared.LockFrame();

        playlist.coursewareRx.Merge(videoPlaylist.coursewareRx).Where(v => v != null).Subscribe(so =>
         {
             Logging.Log("需要播放课程" + so);
             PlayCourseware(so);
         }).AddTo(this);

        API.GetCoursePlayInfo()
            .Subscribe(v => GetAllRoundList(v), (e) =>
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

        GetComponent<CoursewareRoundingList>().Merge();
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

        playingCW = Instantiate(cp.coursewarePlayer, transform);

        /// 初始化数据
        cp.MakeData(playingCW);

        CoursewarePlayer player = playingCW.GetComponent<CoursewarePlayer>();

        if (player == null) { DidEndACourseware(player); return; }

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

        switch (rounding.round.Value.type)
        {
            case RoundIsPlaying.Type.picture:
                playlist.Next();
                break;
            case RoundIsPlaying.Type.video:
                videoPlaylist.Next();
                break;
        }
    }
}