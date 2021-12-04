using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "拼图Player", menuName = "课件/Player/拼图Player")]
public class CW_Puzzle_SO : CoursewarePlayer_SO
{


    /// <summary>
    /// 组装数据
    /// </summary>
    /// <param name="player">GameObject</param>
    /// <param name="data">数据</param>
    /// <returns>是否可以播放</returns>
    public override bool MakeData(GameObject player)
    {
        player.GetComponent<PuzzleManager>().setData(item);
        return true;
    }


    public PuzzleManager.Data item;



    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        var value = CreateInstance<CW_Puzzle_SO>();

        value.coursewarePlayer = coursewarePlayer;

        try
        {
            value.item = content.ToObject<PuzzleManager.Data>();
        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }


        return value;
    }
}
