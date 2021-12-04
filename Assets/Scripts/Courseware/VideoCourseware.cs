using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

using Sirenix.OdinInspector;

using UniRx;

public class VideoCourseware : SerializedMonoBehaviour
{


    //public 

    [System.Serializable]
    public class Range
    {
        public double start = 0;


        public double end = 0;


        public static Range Empty() => new Range();


        public bool Check(double at)
        {


            if (start >= at)
            {
                if (end < at)
                {
                    end = at;
                    return true;
                }
            }
            return false;
        }

    }


    public GameObject videoUI;


    [LabelText("课件列表")]
    public List<CW_OriginContent> playlist = new List<CW_OriginContent>();


    public MediaPlayer videoPlayer;

    public CoursewareManager cwManager;

    [ShowInInspector, ReadOnly, ListDrawerSettings(Expanded = true)]
    public List<CW_OriginContent.Joint> joints = new List<CW_OriginContent.Joint>();


    public CoursewareSupportList supportsList;

    [ShowInInspector]
    public Range vv = new Range();


    public void SetRound(RoundIsPlaying round)
    {
        gameObject.SetActive(true);

        videoPlayer.OpenMedia(new MediaPath(round.src, MediaPathType.AbsolutePathOrURL), autoPlay: true);
        //videoPlayer.MediaSource

        joints.Clear();

        joints.AddRange(round.process.Select(v => v.joint));


        var supports = supportsList.supports;

        playlist.Clear();
        playlist.AddRange(round.process);

    }

    public bool Continue()
    {

        if (videoPlayer.Control.IsFinished()) return false;

        videoUI.SetActive(true);

        videoPlayer.Play();

        return true;
    }


    void Update()
    {

        if (videoPlayer.Control.IsFinished())
        {
            videoPlayer.CloseMedia();
            gameObject.SetActive(false);
            cwManager.NextRound();
            return;
        }


        if (videoPlayer.Control.IsPaused()) return;


        vv.start = videoPlayer.Control.GetCurrentTime();

        foreach (var list in playlist)
        {
            if (vv.Check(list.joint.at))
            {

                var so = supportsList.suppoting(list.type);

                if (so != null)
                {
                    videoPlayer.Pause();

                    videoUI.SetActive(false);

                    var newSo = so.ParseData(list.content);
                    cwManager.PlayCourseware(newSo);

                }
            }
        }

    }
}
