using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class CoursewarePlaylist : MonoBehaviour
{


    [LabelText("课件列表")]
    public List<CoursewarePlayer_SO> playlist;

    //[SerializeField]
    //[ReadOnly]
    public int index = -1;

    [LabelText("还有需要播放的课件")]
    [ShowInInspector]
    bool playable
    {
        get
        {
            if (playlist == null || playlist.Count == 0) return false;
            return index < playlist.Count;
        }
    }

    [LabelText("最后一个课件")]
    [ShowInInspector]
    bool last
    {
        get
        {
            if (!playable) return false;
            return index == playlist.Count - 1;
        }
    }

    [LabelText("第一个课件")]
    [ShowInInspector]
    bool first
    {
        get
        {
            if (!playable) return false;
            return index == 0;
        }
    }


    [LabelText("正在播放的课件")]
    [ReadOnly]
    [SerializeField]
    CoursewarePlayer_SO courseware;

    public CoursewarePlayer_SO Next()
    {

        if (!last)
        {
            index++;
            courseware = playlist[index];
        }
        else
        {
            courseware = null;
        }



        return courseware;
    }


    public CoursewarePlayer_SO Previous()
    {
        if (first)
        {
            courseware = null;
        }
        else
        {
            courseware = playlist[--index];
        }

        return courseware;
    }


}
