using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;

public class CoursewarePlaylist : SerializedMonoBehaviour
{



    [LabelText("课件列表")]
    public List<CoursewarePlayer_SO> playlist = new List<CoursewarePlayer_SO>();

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


    private void Start()
    {
        API.GetCoursePlayInfo().Subscribe(v =>
        {
            //var process = v.rounds[0].process[0].process;
            //var a = GetComponent<CoursewareSupportList>().supports[process.type];
            //a.ParseData(process.content);
            //playlist.Add(a);
        }, (e) =>
        {
            Logging.Log((e as HttpError).message);
        });
    }


    public CoursewarePlayer_SO Next()
    {

        if (playlist.Count == 0) return null;

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
