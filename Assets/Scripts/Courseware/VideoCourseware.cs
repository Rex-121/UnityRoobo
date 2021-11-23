using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class VideoCourseware : MonoBehaviour
{

    public MediaPlayer videoPlayer;

    public CoursewareManager cwManager;

    void Update()
    {

        if (videoPlayer.Control.IsPaused()) return;

        Logging.Log(videoPlayer.Control.GetCurrentTime());
        if (videoPlayer.Control.GetCurrentTime() >= 5)
        {
            videoPlayer.Pause();
            cwManager.Play();
            gameObject.SetActive(false);
        }

    }
}
