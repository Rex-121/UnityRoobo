using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "暂停(Round)Player", menuName = "课件/Player/暂停(Round)Player")]
public class CW_JustPause_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {
        player.GetComponent<CW_JustPause>().SetImageData(image);
        return true;
    }

    public Image image;

    public struct Image
    {
        public string image;

        public Image(string image)
        {
            this.image = image;
        }
    }
    public override CoursewarePlayer_SO ParseData(JToken content)
    {

        var d = CreateInstance<CW_JustPause_SO>();
        d.coursewarePlayer = coursewarePlayer;
        Logging.Log("Jlfasjdflajslga");

        Logging.Log(content);

        d.image = content.ToObject<Image>();

        Logging.Log(d.image);
        return d;
    }

}
