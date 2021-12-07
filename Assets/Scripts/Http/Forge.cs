using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Newtonsoft.Json.Converters;

public class Forge
{

    [JsonObject(MemberSerialization.Fields)]
    public struct Data
    {
        [JsonProperty, JsonRequired]
        public int result;// "result": 0,


        public bool success => result == 0;

        public string msg;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string desc;


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public JToken data;
    }

    public static Data ParseNet(string jsonString)
    {
        return JsonConvert.DeserializeObject<Data>(jsonString);
    }


    public static void Check(string jsonString)
    {

        Logging.Log("Forge Check");

        try
        {
            var dic = JsonConvert.DeserializeObject<Data>(jsonString);


            Logging.Log(dic.result + " " + dic.msg);

            Logging.Log(dic.data);
            var aa = dic.data.ToObject<ForgeData.RoundList>();

            //Logging.Log(aa.course.name);

            List<Round> r = new List<Round>(aa.list.Count);

            foreach (var v in aa.list)
            {
                r.Add(new Round(v));
            }

            var rQueue = new RoundQueue(r);

            Logging.Log(rQueue.FlowDescription());


            //Logging.Log(dic.result + " " + dic.msg);

            //Logging.Log(dic.data);
            //var aa = dic.data.ToObject<ForgeData.Course>();

            //Logging.Log(aa.course.name);

            //List<Round> r = new List<Round>(aa.rounds.Count);

            //foreach (var v in aa.rounds)
            //{
            //    r.Add(new Round(v));
            //}

            //var rQueue = new RoundQueue(r);

            //Logging.Log(rQueue.FlowDescription());

            /// 勿删
            //foreach (var p in aa.rounds)
            //{


            //    Logging.Log(p.type + "(" + p.name + ")" + p.displayMode + "--" + p.endAction);

            //    switch (p.type)
            //    {
            //        case ForgeData.Rounds.Type.Picture:
            //            var pc = p.content.ToObject<List<ForgeData.PictureRound>>();

            //            string f = "";

            //            foreach (var k in pc)
            //            {
            //                f += ("-> " + k.process.type);
            //            }

            //            Logging.Log(f);
            //            break;
            //        case ForgeData.Rounds.Type.Video:

            //            var vRound = p.content.ToObject<ForgeData.VideoRound>();


            //            string vf = "";

            //            foreach (var k in vRound.stopPoints)
            //            {
            //                vf += ("-> " + k.process.type + ":at:" + k.at);
            //            }

            //            Logging.Log(vf);

            //            break;
            //    }
            //}

        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }


        Logging.Log("Forge Checked");
    }




}



/// --------------------------------------------------------------------------------------------------

public class RoundQueue
{

    public List<Round> rounds;

    public RoundQueue(List<Round> r)
    {
        rounds = r;
    }

}


public struct Round
{

    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    public string icon;

    public int id;

    public ForgeData.Rounds.DisplayMode displayMode;

    public ForgeData.Rounds.Pipeline pipeline;


    public ForgeData.Rounds.Type type;

    public Round(ForgeData.Rounds round)
    {
        id = round.id;
        name = round.name;
        displayMode = round.displayMode;
        icon = round.icon;
        pipeline = round.endAction;

        type = round.type;


        playlist = new List<RoundIsPlaying>();

        playlist.AddRange(RoundQueueParse.ParseQueue(this, round));


    }

    public List<RoundIsPlaying> playlist;



    struct Picture
    {
        public string src;

        public ForgeData.RoundProcess.Process process;
    }

}







public static class RoundExtensions
{
    /// <summary>
    /// 是否展示在界面上
    /// </summary>
    public static bool DisplayOnLand(this Round o)
    {
        return o.displayMode == ForgeData.Rounds.DisplayMode.common;
    }

    /// <summary>
    /// Round的基本属性
    /// </summary>
    public static string DebugDescription(this Round o)
    {
        var display = o.DisplayOnLand() ? "展示" : "不展示";
        return o.name + "--(" + display + "/" + o.pipeline + ")";
    }

    /// <summary>
    /// Round的基本属性
    /// </summary>
    public static string FlowDescription(this RoundQueue o)
    {

        var s = "";


        foreach (var round in o.rounds)
        {
            s += ("|-> " + round.DebugDescription() + "\n");
        }


        return s;
    }
}






/// <summary>
/// 原始数据
/// </summary>
public class CW_OriginContent
{

    public JToken content;

    public CoursewareType type;

    /// <summary>
    ///  节点
    /// </summary>
    public Joint joint;

    public struct Joint
    {
        public int at;

        public Joint(int at)
        {
            this.at = at;
        }

        public static Joint Empty() => new Joint(0);

    }


    public CW_OriginContent(CoursewareType type, JToken token, Joint joint)
    {
        this.type = type;
        this.joint = joint;
        content = token;
    }


    public CW_OriginContent next;
    public CW_OriginContent previous;


    public static CW_OriginContent Empty()
    {
        return new CW_OriginContent(CoursewareType.unknow, null, Joint.Empty());
    }


    public CW_OriginContent FindAt(int at)
    {
        if (at == joint.at) return this;

        if (next != null) return next.FindAt(at);

        return null;
    }
}