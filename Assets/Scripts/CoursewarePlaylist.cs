using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;

public class CoursewarePlaylist : CoursewareBasicPlaylist
{

    public override void RoundDidChanged()
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


    int index = -1;

    public override string description => courseware != null ? "正在播放第" + (index + 1) + "个" : "无播放课件";

    [Title("列表", "$description"), LabelText("课件列表"), PropertySpace(SpaceAfter = 30)]
    public List<CoursewarePlayer_SO> playlist = new List<CoursewarePlayer_SO>();


    [LabelText("还有需要播放的课件")]
    bool playable
    {
        get
        {
            if (playlist == null || playlist.Count == 0) return false;
            return index < playlist.Count;
        }
    }

    [LabelText("最后一个课件")]
    bool last
    {
        get
        {
            if (!playable) return false;
            return index == playlist.Count - 1;
        }
    }

    [LabelText("第一个课件")]
    bool first
    {
        get
        {
            if (!playable) return false;
            return index == 0;
        }
    }



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
