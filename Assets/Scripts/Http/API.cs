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
            var queue = UpdateRoundValue(v.rounds);
            Logging.Log("fasfdas" + queue.rounds[0].process[0].process.type);
            return queue;
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
}


//public struct Rounds
//{
//    public List<RoundProcess> process { private set; get; }

//    public Rounds(List<RoundProcess> process)
//    {
//        this.process = process;
//    }


//    public string queue
//    {
//        get
//        {

//            string v = "[\n";
//            foreach (var pro in process)
//            {
//                v += (pro.value.type + " -> ");
//            }
//            return v + "\n]";
//        }
//    }
//}


//public struct RoundProcess
//{

//    public struct Key
//    {

//        public string src;


//        public ForgeData.Rounds.Type roundType;

//        public Key(string src, ForgeData.Rounds.Type roundType)
//        {
//            this.src = src;
//            this.roundType = roundType;
//        }

//        public static Key Picture(string src)
//        {
//            return new Key(src, ForgeData.Rounds.Type.Picture);
//        }
//    }


//    public struct Value
//    {
//        public JToken content;

//        public string type;
//    }

//    public Key key;

//    [JsonProperty("process")]
//    public Value value;

//}


//static class ParseRoundData
//{

//    public static Rounds Parse(List<ForgeData.Rounds> rounds)
//    {
//        foreach (var round in rounds)
//        {

//            switch (round.type)
//            {
//                case ForgeData.Rounds.Type.Picture:

//                    return PictureRound(round.content);

//            }

//        }

//        return new Rounds();
//    }


//    static Rounds PictureRound(JToken content)
//    {
//        return new Rounds(content.ToObject<List<RoundProcess>>());
//    }

//}
