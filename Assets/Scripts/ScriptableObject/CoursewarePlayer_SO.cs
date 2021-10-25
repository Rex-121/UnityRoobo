using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CoursewarePlayer_SO : ScriptableObject
{

    [LabelText("课件Prefab")]
    public GameObject coursewarePlayer;


    public dynamic data;



    /// <summary>
    /// 组装数据
    /// </summary>
    /// <param name="player">GameObject</param>
    /// <param name="data">数据</param>
    /// <returns>是否可以播放</returns>
    public virtual bool MakeData(GameObject player, dynamic data)
    {

        return false;
    }

}
