
using Newtonsoft.Json.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "跟读Player", menuName = "课件/Player/跟读Player")]
public class CW_FollowRead_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {

        return true;
    }



    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        var value = CreateInstance<CW_FollowRead_SO>();

        value.coursewarePlayer = coursewarePlayer;
        return value;
    }
}
