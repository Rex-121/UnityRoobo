using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "圈图点读Player", menuName = "课件/Player/圈图点读Player")]
public class CW_Seek_SO : CoursewarePlayer_SO
{

    /// <summary>
    /// 组装数据
    /// </summary>
    /// <param name="player">GameObject</param>
    /// <param name="data">数据</param>
    /// <returns>是否可以播放</returns>
    public override bool MakeData(GameObject player)
    {
        //player.GetComponent<PoemManager>().setData(item);
        return true;
    }


    public PoemManager.Data item;



    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        var value = CreateInstance<CW_Seek_SO>();

        value.coursewarePlayer = coursewarePlayer;

        try
        {
            value.item = content.ToObject<PoemManager.Data>();
        }
        catch (System.Exception e)
        {
            Logging.Log(e.Message);
        }


        return value;
    }
}
