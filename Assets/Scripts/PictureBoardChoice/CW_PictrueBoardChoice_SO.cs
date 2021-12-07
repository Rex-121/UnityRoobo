using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "画板选择题Player", menuName = "课件/Player/画板选择题Player")]
public class CW_PictrueBoardChoice_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {
        player.GetComponent<CW_PictureBoardChoice>().InitGridAndData(item);
        return true;
    }

    public PictureBoardEntity item;
    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        Debug.Log("CW_PictrueBoardChoice_SO= " + content);
        var d = CreateInstance<CW_PictrueBoardChoice_SO>();
        d.coursewarePlayer = coursewarePlayer;
        d.item = content.ToObject<PictureBoardEntity>();
        return d;
    }

    public class PictureBoardEntity { }
}
