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

    [ShowInInspector]
    public override RoundIsPlaying.Type type => RoundIsPlaying.Type.empty;


    public RoundIsPlaying RemoveSelf()
    {
        next.previous = null;
        return next;
    }

}


public class PauseRound : RoundIsPlaying
{

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



[System.Serializable]
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

    [ShowInInspector, PropertyOrder(100)]
    public RoundIsPlaying next { get; set; }

    [ShowInInspector, PropertyOrder(101)]
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


    string RoundInLineInInspector
    {
        get
        {
            Debug.Log(next == null);
            if (next == null) return ProcessDebugDescription;
            return ProcessDebugDescription + next.RoundInLineInInspector;
        }
    }


    [Button("LogQueue")]
    private void LogQueue()
    {
        Debug.Log(RoundInLineInInspector);
    }

    string ProcessDebugDescription
    {

        get
        {
            string processType = "";
            foreach (var i in process)
            {
                processType += "->";
                processType += i.type;
            }

            var value = type.ToString() + "-(-" + processType + "-)-";
            return value;
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

    ARound(string s, List<CW_OriginContent> p, RoundIsPlaying.Type _type)
    {
        src = s;
        process = p;
        this._type = _type;
        next = null;
        previous = null;
    }
    public static RoundIsPlaying Picture(string s, List<CW_OriginContent> p)
    {
        return new ARound(s, p, RoundIsPlaying.Type.picture);
    }

    public static RoundIsPlaying Video(string s, List<CW_OriginContent> p)
    {
        return new ARound(s, p, RoundIsPlaying.Type.video);
    }
}

