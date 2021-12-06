using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;

[RequireComponent(typeof(CoursewareSupportList), typeof(CoursewareManager))]
public class CoursewareBasicPlaylist : SerializedMonoBehaviour
{

    [HideInInspector]
    public CoursewareManager cwManager;


    [LabelText("支持的课件列表"), HideInInspector]
    public CoursewareSupportList supports;

    private void Awake()
    {
        cwManager = GetComponent<CoursewareManager>();
        supports = GetComponent<CoursewareSupportList>();
    }

    RoundIsPlaying _Round;

    [HideInInspector]
    public RoundIsPlaying round
    {
        set
        {
            _Round = value;

            RoundDidChanged();
        }
        get
        {
            return _Round;
        }
    }


    public virtual void RoundDidChanged() { }

    [HideInInspector]
    public virtual string description { get; }


    [Title("正在播放的课件", "$description"), HideLabel]
    [ReadOnly]
    [SerializeField]
    public CoursewarePlayer_SO courseware;

    //CoursewarePlayer_SO ReMakeSO(int index)
    //{

    //    var data = round.process[index].content;


    //    return playlist[index].ParseData(data);

    //}


}
