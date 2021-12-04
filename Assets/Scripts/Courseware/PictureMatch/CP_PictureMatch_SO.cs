using Newtonsoft.Json.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "连线Player", menuName = "课件/Player/连线Player")]
public class CP_PictureMatch_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {
        return true;
    }


    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        var value = CreateInstance<CP_PictureMatch_SO>();

        value.coursewarePlayer = coursewarePlayer;
        return value;
    }
}
