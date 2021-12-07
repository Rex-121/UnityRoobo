using RenderHeads.Media.AVProVideo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class VideoAspectScript : MonoBehaviour
{

    public string videoUrl;

    public System.Action<string> onClick;


    [LabelText("重播按钮")]
    public Button mReplayButton;


    [LabelText("播放器")]
    public MediaPlayer mPlayer;


    public void OnClickToMoteScale()
    {
        onClick(this.videoUrl);
    }


}
