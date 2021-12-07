using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class RoundQueueParse
{



    public static List<RoundIsPlaying> ParseQueue(Round round, ForgeData.Rounds originRound)
    {

        var list = new List<RoundIsPlaying>();
        try
        {
            switch (round.type)
            {
                case ForgeData.Rounds.Type.Picture:
                case ForgeData.Rounds.Type.PicBook:
                    list.AddRange(Picture(originRound.processList));
                    break;
                case ForgeData.Rounds.Type.Video:
                    list.AddRange(Video(originRound));
                    break;
            }



        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }

        try
        {
            switch (round.pipeline)
            {
                case ForgeData.Rounds.Pipeline.pause:

                    var image = new CW_JustPause_SO.Image(round.pauseImage);

                    JToken aka = JsonConvert.DeserializeObject<JToken>(JsonConvert.SerializeObject(image));

                    CW_OriginContent con = new CW_OriginContent(CoursewareType.justPause, aka, CW_OriginContent.Joint.Empty());

                    var pauseRound = new PauseRound(new List<CW_OriginContent> { con });

                    list.Add(pauseRound);
                    break;
            }

        }
        catch
        {
            return list;
        }

        return list;
    }


    static List<RoundIsPlaying> Picture(List<ForgeData.RoundProcess> process)
    {

        List<RoundIsPlaying> list = new List<RoundIsPlaying>();

        try
        {



            foreach (var p in process)
            {
                list.Add(ARound.Picture(p.src, new List<CW_OriginContent>() { p.cw_OriginContent() }));
            }

            foreach (var round in list)
            {

            }

        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }

        return list;
    }


    public static List<RoundIsPlaying> Video(ForgeData.Rounds originRound)
    {


        var process = originRound.videoProcess;
        var ss = process.stopPoints.Select(v => v.cw_OriginContent()).ToList();
        List<RoundIsPlaying> list = new List<RoundIsPlaying>();

        list.Add(ARound.Video(process.video, ss));

        return list;
    }


}


public class LeadingRound : RoundIsPlaying
{
    //public string src { get; set; }

    //public List<CW_OriginContent> process { get; set; }

    [ShowInInspector]
    public override RoundIsPlaying.Type type => RoundIsPlaying.Type.empty;

    //public RoundIsPlaying next { get; set; }

    //public RoundIsPlaying previous { get; set; }


    public RoundIsPlaying RemoveSelf()
    {
        next.previous = null;
        return next;
    }

    //public int count
    //{
    //    get
    //    {
    //        if (next == null) return 1;
    //        return 1 + next.count;
    //    }
    //}

    //public string des
    //{
    //    get
    //    {
    //        if (next == null) return type.ToString();
    //        return type.ToString() + next.des;
    //    }
    //}

}


public class PauseRound : RoundIsPlaying
{
    //public string src { get; set; }

    //public List<CW_OriginContent> process { get; set; }
    //[ShowInInspector]
    //public override List<CW_OriginContent> process;

    [ShowInInspector]
    public override RoundIsPlaying.Type type => RoundIsPlaying.Type.pause;


    public PauseRound(List<CW_OriginContent> p)
    {
        src = "";
        process = p;

        Logging.Log(process.Count);
        next = null;
        previous = null;
    }

}




public class RoundIsPlaying
{
    //public string src;

    [ShowInInspector]
    public List<CW_OriginContent> process = new List<CW_OriginContent>();

    public virtual Type type { get; }

    public enum Type
    {
        video, picture, pop, pause, empty
    }

    [ShowInInspector]
    public RoundIsPlaying next { get; set; }

    [ShowInInspector]
    public RoundIsPlaying previous { get; set; }

    [ShowInInspector]
    public int count
    {
        get
        {
            if (next == null) return 1;
            return 1 + next.count;
        }
    }


    [ShowInInspector]
    public string des
    {
        get
        {
            if (next == null) return type.ToString();
            return type.ToString() + next.des;
        }
    }

    [ShowInInspector, LabelText("$type"), LabelWidth(50)]
    public string src { get; set; }
}

public class ARound : RoundIsPlaying
{


    public override Type type => _type;


    [ShowInInspector, HideLabel]
    List<CoursewareType> types => process.Select(v => v.type).ToList();


    protected Type _type;
    //[ShowInInspector]
    //public int count
    //{
    //    get
    //    {
    //        if (next == null) return 1;
    //        return 1 + next.count;
    //    }
    //}

    //[ShowInInspector]
    //public string des
    //{
    //    get
    //    {
    //        var types = process.Select(v => v.type.ToString());

    //        var i = "";

    //        types.ForEach(v =>
    //        {
    //            i += (v + "->");
    //        });

    //        var value = "|-------" + type.ToString() + " (" + i + ") " + "-------|\n";

    //        if (next == null) return value;
    //        return value + next.des;
    //    }
    //}


    //[ShowInInspector, ReadOnly]
    //public List<CW_OriginContent> process { get; set; }

    ARound(string s, List<CW_OriginContent> p, RoundIsPlaying.Type _type)
    {
        src = s;
        process = p;
        this._type = _type;
        next = null;
        previous = null;
    }

    //[ShowInInspector, ReadOnly]
    //public RoundIsPlaying next { get; set; }

    //[ShowInInspector, ReadOnly]
    //public RoundIsPlaying previous { get; set; }

    public static RoundIsPlaying Picture(string s, List<CW_OriginContent> p)
    {
        return new ARound(s, p, RoundIsPlaying.Type.picture);
    }

    public static RoundIsPlaying Video(string s, List<CW_OriginContent> p)
    {
        return new ARound(s, p, RoundIsPlaying.Type.video);
    }
}

