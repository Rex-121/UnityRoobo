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
        return false;
    }


    public PuzzleManager.Data item;


    public override bool ParseData(JToken content)
    {
        try
        {
            item = content.ToObject<PuzzleManager.Data>();

            Logging.Log("widht" + item.width);
        } catch (Exception e)
        {
            Logging.Log(e.Message);
        }

       

        return true;
    }

}
