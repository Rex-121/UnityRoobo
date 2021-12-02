using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json.Linq;

public class CoursewarePlayer_SO : SerializedScriptableObject
{

    [LabelText("课件Prefab")]
    public GameObject coursewarePlayer;

    /// <summary>
    /// 组装数据
    /// </summary>
    /// <param name="player">GameObject</param>
    /// <param name="data">数据</param>
    /// <returns>是否可以播放</returns>
    public virtual bool MakeData(GameObject player)
    {

        return false;
    }


    public virtual bool ParseData(JToken content)
    {
        return true;
    }

}
