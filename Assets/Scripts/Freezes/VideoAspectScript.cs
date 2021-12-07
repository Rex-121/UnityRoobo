using RenderHeads.Media.AVProVideo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class VideoAspectScript : MonoBehaviour
{

    public string videoUrl;

    //public System.Action<GameObject> onClick;

    public System.Action<string> onClick;

    [LabelText("重播按钮")]
    public Button mReplayButton;

    [LabelText("播放器")]
    public MediaPlayer mPlayer;


    public void OnClickToMoteScale()
    {
        //onClick(this.gameObject);
        onClick(this.videoUrl);
    }

    //public void OnClickToReplay(MediaPlayer player)
    //{
    //    if (!string.IsNullOrEmpty(videoUrl))
    //    {
    //        MediaPath path = new MediaPath(videoUrl, MediaPathType.AbsolutePathOrURL);
    //        player.OpenMedia(path, false);
    //    }
    //}
}
