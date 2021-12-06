using System.Collections.Generic;
using System.Linq;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

using Sirenix.OdinInspector;

using UniRx;

public class CoursewareVideoPlaylist : CoursewareBasicPlaylist
{
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

    [PropertyOrder(100)]
    public GameObject videoUI;



    public override string description => currentJoint != null ? "正在播放第" + currentJoint.Value.at + "秒" : "无播放课件";

    [Title("列表", "$description"), LabelText("课件列表"), PropertySpace(SpaceAfter = 30)]
    public List<CW_OriginContent> playlist = new List<CW_OriginContent>();

    [PropertyOrder(101)]
    public MediaPlayer videoPlayer;



    CW_OriginContent.Joint? currentJoint;

    [ShowInInspector, ReadOnly, ListDrawerSettings(Expanded = true)]
    public List<CW_OriginContent.Joint> joints = new List<CW_OriginContent.Joint>();


    //public CoursewareSupportList supportsList;

    [ShowInInspector]
    public Range vv = new Range();


    public override void RoundDidChanged()
    {
        videoPlayer.gameObject.SetActive(true);

        videoPlayer.OpenMedia(new MediaPath(round.src, MediaPathType.AbsolutePathOrURL), autoPlay: true);
        //videoPlayer.MediaSource

        joints.Clear();

        joints.AddRange(round.process.Select(v => v.joint));


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

        if (videoPlayer.Control == null) return;

        if (videoPlayer.Control.IsFinished())
        {
            videoPlayer.CloseMedia();
            videoPlayer.gameObject.SetActive(false);
            cwManager.NextRound();
            return;
        }


        if (videoPlayer.Control.IsPaused()) return;


        vv.start = videoPlayer.Control.GetCurrentTime();

        foreach (var list in playlist)
        {
            if (vv.Check(list.joint.at))
            {

                var so = supports.suppoting(list.type);

                if (so != null)
                {
                    videoPlayer.Pause();

                    videoUI.SetActive(false);

                    currentJoint = list.joint;

                    courseware = so.ParseData(list.content);
                    cwManager.PlayCourseware(courseware);

                }


            }
            else
            {
                courseware = null;
            }

        }

    }
}
