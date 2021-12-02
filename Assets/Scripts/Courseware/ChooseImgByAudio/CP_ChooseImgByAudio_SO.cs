
using UnityEngine;

[CreateAssetMenu(fileName = "音选图Player", menuName = "课件/Player/音选图Player")]
public class CP_ChooseImgByAudio_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {
        var play = player.GetComponent<CP_ChooseImgByAudio>();

        play.template = ChooseImgByAudioTemplate.Farm;

        return true;
    }
}
