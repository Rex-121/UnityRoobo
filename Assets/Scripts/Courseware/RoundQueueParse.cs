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
                list.Add(new PictureRound(p.src, new List<CW_OriginContent>() { p.cw_OriginContent() }));
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

        list.Add(new PictureRound(process.video, ss));

        return list;
    }


}


public struct EmptyRound : RoundIsPlaying
{
    public string src { get; set; }

    public List<CW_OriginContent> process { get; set; }


    public string des
    {
        get
        {
            return "空转-> ";
        }
    }

}


public interface RoundIsPlaying
{
    public string src { get; set; }

    [ShowInInspector]
    public string des { get; }

    public List<CW_OriginContent> process { get; set; }
}

public struct PictureRound : RoundIsPlaying
{
    [ShowInInspector, LabelText("$des"), LabelWidth(50)]
    public string src { get; set; }

    public string des => "图片";




    [ShowInInspector, HideLabel]
    List<CoursewareType> types
    {
        get
        {
            return process.Select(v => v.type).ToList();
        }
    }


    public List<CW_OriginContent> process { get; set; }

    public PictureRound(string s, List<CW_OriginContent> p)
    {
        src = s;
        process = p;
    }

}

