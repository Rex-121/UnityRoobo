using System.Collections.Generic;
using System.Linq;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

using Sirenix.OdinInspector;

using UniRx;
using System;

public class CoursewareVideoPlaylist : CoursewareBasicPlaylist
{ 


    public override bool SupportRountType(RoundIsPlaying.Type roundType) { return roundType == RoundIsPlaying.Type.video; }



    [PropertyOrder(100)]
    public GameObject videoUI;



    public override string description => passed.Count != 0 ? "正在播放第" + passed.Last() + "秒" : "无播放课件";


    [PropertyOrder(101)]
    public MediaPlayer videoPlayer;


    public override void RoundDidLoaded(RoundIsPlaying round)
    {
        base.RoundDidLoaded(round);

        passed.Clear();

        videoPlayer.gameObject.SetActive(true);

        videoPlayer.OpenMedia(new MediaPath(round.src, MediaPathType.AbsolutePathOrURL), autoPlay: true);
    }



    public bool Continue()
    {

        if (videoPlayer.Control.IsFinished()) return false;

        videoUI.SetActive(true);

        videoPlayer.Play();

        return true;
    }

    public override void Next()
    {


        Observable.EveryEndOfFrame().Take(1).Subscribe(_ => videoPlayer.Play());

    }

    void Update()
    {

        if (videoPlayer.Control == null) return;

        if (videoPlayer.Control.IsFinished())
        {
            videoPlayer.CloseMedia();
            videoPlayer.gameObject.SetActive(false);
            NextRound();
            return;
        }


        if (videoPlayer.Control.IsPaused()) return;


        var time = (int)Math.Floor(videoPlayer.Control.GetCurrentTime());

        var pass = passed.Contains(time);

        if (pass) return;

        if (playlist == null) return;

        var find = playlist.FindAt(time);

        if (find == null) return;

        PlayCourseware(find);

    }

    [ReadOnly]
    public HashSet<int> passed = new HashSet<int>();


    public override void PlayCoursewareAutomatic(CW_OriginContent pl)
    {

    }

    public override bool PlayCourseware(CW_OriginContent pl)
    {

        var so = ReMakeSO(pl);

        if (so != null)
        {
            coursewareRx.OnNext(so);

            passed.Add(pl.joint.at);

            videoPlayer.Pause();

            return true;
        }

        return false;
    }

}
