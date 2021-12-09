using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using System;
using Newtonsoft.Json.Linq;

using Newtonsoft.Json;
using System.ComponentModel;
using Newtonsoft.Json.Converters;
public class API
{
    /// <summary>
    /// 获取Course播放列表
    /// </summary>
    public static IObservable<RoundQueue> GetCoursePlayInfo()
    {
        return HttpRx.Get<ForgeData.Course>("/pudding/teacher/v1/course/5509/lesson/7196/play/info").Select(v =>
        {
            return UpdateRoundValue(v.rounds);
        });
    }

    static RoundQueue UpdateRoundValue(List<ForgeData.Rounds> rList)
    {
        List<Round> r = new List<Round>(rList.Count);

        foreach (var v in rList)
        {
            r.Add(new Round(v));
        }

        var rQueue = new RoundQueue(r);

        Logging.Log(rQueue.FlowDescription());

        return rQueue;
    }


    public static IObservable<CourseLevels_Net> GetCourseLevelsByClassCategory(ClassCategory category)
    {

        string api = "/pudding/teacher/v1/levelcourse/subject/" + category.subject.NetRepresent + "/type/" + category.NetRepresent + "/level/list";
        Logging.Log(api);
        return HttpRx.Get<CourseLevels_Net>(api);
    }

}

