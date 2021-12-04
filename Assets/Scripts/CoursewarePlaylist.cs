using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;

public class CoursewarePlaylist : SerializedMonoBehaviour
{

    RoundIsPlaying _Round;

    [HideInInspector]
    public RoundIsPlaying round
    {
        set
        {
            _Round = value;

            SS();
        }
        get
        {
            return _Round;
        }
    }


    public void SS()
    {

        if (round == null) return;

        index = -1;

        var supports = GetComponent<CoursewareSupportList>().supports;


        var list = round.process.Select(v =>
        {

            if (supports.ContainsKey(v.type))
            {
                return supports[v.type];
            }

            return null;
        }).Where(v => v != null).ToList();


        playlist.Clear();

        playlist.AddRange(list);

    }

    [LabelText("课件列表")]
    public List<CoursewarePlayer_SO> playlist = new List<CoursewarePlayer_SO>();


    //[SerializeField]
    [ReadOnly]
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

        if (playlist.Count == 0) return null;

        if (!last)
        {
            index++;

            courseware = ReMakeSO(index);

            if (courseware == null) return Next();

        }
        else
        {
            courseware = null;
        }



        return courseware;
    }


    //TODO:未完成
    public CoursewarePlayer_SO Previous()
    {
        if (first)
        {
            courseware = null;
        }
        else
        {
            courseware = ReMakeSO(--index);
        }

        return courseware;
    }

    CoursewarePlayer_SO ReMakeSO(int index)
    {

        var data = round.process[index].content;


        return playlist[index].ParseData(data);

    }


}
