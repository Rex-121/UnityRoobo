using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class RoundQueueParse
{



    public static List<RoundIsPlaying> ParseQueue(Round round, ForgeData.Rounds originRound)
    {
        try
        {
            switch (round.type)
            {
                case ForgeData.Rounds.Type.Picture:
                case ForgeData.Rounds.Type.PicBook:
                    return Picture(originRound.processList);
                case ForgeData.Rounds.Type.Video:
                    return Video(originRound);
            }
        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }

        return new List<RoundIsPlaying>();
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


public struct EmptyRound : RoundIsPlaying
{
    public string src { get; set; }

    public List<CW_OriginContent> process { get; set; }

    public RoundIsPlaying.Type type => RoundIsPlaying.Type.empty;
}


public interface RoundIsPlaying
{
    public string src { get; }

    public List<CW_OriginContent> process { get; set; }

    public Type type { get; }

    public enum Type
    {
        video, picture, empty, highFive
    }
}

public struct ARound : RoundIsPlaying
{
    [ShowInInspector, LabelText("$type"), LabelWidth(50)]
    public string src { get; set; }

    public RoundIsPlaying.Type type => _type;

    RoundIsPlaying.Type _type;

    [ShowInInspector, HideLabel]
    List<CoursewareType> types => process.Select(v => v.type).ToList();


    public List<CW_OriginContent> process { get; set; }

    ARound(string s, List<CW_OriginContent> p, RoundIsPlaying.Type _type)
    {
        src = s;
        process = p;
        this._type = _type;
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

