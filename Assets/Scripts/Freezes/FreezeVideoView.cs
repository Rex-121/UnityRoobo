using RenderHeads.Media.AVProVideo;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FreezeVideoView : MonoBehaviour
{

    [ShowInInspector]
    [LabelText("顶部Grid")]
    public GridLayoutGroup mTopGrid;

    [ShowInInspector]
    [LabelText("底部Grid")]
    public GridLayoutGroup mBottomGrid;

    [LabelText("中部Grid")]
    public GridLayoutGroup mCenterGrid;

    [LabelText("视频遮罩")]
    public Transform mShadow;

    [LabelText("视频播放器")]
    public MediaPlayer mShadowPlayer;

    [LabelText("视频重播按钮")]
    public Button mShadowReplayButton;

    public GameObject prefabVideo;

    public bool showRepeat;
    /// <summary>
    /// 初始化显示内容
    /// </summary>
    /// <param name="imageUrls"></param>
    public void InitGrids(List<CW_Freeze_SO.FreezeEntity.Video> videos)
    {
        Debug.Log("FreezeVideoView=>" + videos.Count);
        if (videos.Count > 0)
        {
            if (videos.Count <= 4)
            {
                mCenterGrid.gameObject.SetActive(true);
                mTopGrid.gameObject.SetActive(false);
                mBottomGrid.gameObject.SetActive(false);
            }
            else
            {
                mCenterGrid.gameObject.SetActive(false);
                mTopGrid.gameObject.SetActive(true);
                mBottomGrid.gameObject.SetActive(true);
            }
            InitVideos(videos);
        }
        else
        {
            mCenterGrid.gameObject.SetActive(false);
            mTopGrid.gameObject.SetActive(false);
            mBottomGrid.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 放置数据
    /// </summary>
    /// <param name="videos"></param>
    private void InitVideos(List<CW_Freeze_SO.FreezeEntity.Video> videos)
    {
        Debug.Log("FreezeImageView=InitImages>>" + videos.Count);
        if (videos.Count <= 4)
        {
            videos.ForEach(e =>
            {
                StartLoadVideo(mCenterGrid, e.video);
            });
        }
        else
        {
            int counts = videos.Count;
            if (videos.Count > 8)
            {
                counts = 8;
            }

            int topCount = (counts + 1) / 2;

            List<CW_Freeze_SO.FreezeEntity.Video> topUrls = videos.GetRange(0, topCount);

            topUrls.ForEach(e => {
                StartLoadVideo(mTopGrid, e.video);
            });

            List<CW_Freeze_SO.FreezeEntity.Video> buttomUrls = videos.GetRange(topCount, counts - topCount);
            buttomUrls.ForEach(e => {
                StartLoadVideo(mBottomGrid, e.video);
            });

        }
    }


    /// <summary>
    /// 添加到grid ,并加载网络图片
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public void StartLoadVideo(GridLayoutGroup grid, string url)
    {
        Debug.Log("StartLoadVideo" + grid + " " + grid.name+" path:"+url);
        GameObject prefab = Instantiate(prefabVideo, grid.transform);
        prefab.GetComponent<VideoAspectScript>().onClick += OnClickToMoteScale;
        prefab.GetComponent<VideoAspectScript>().videoUrl = url;
        var player = prefab.GetComponentInChildren<MediaPlayer>();
        MediaPath path = new MediaPath(url,MediaPathType.AbsolutePathOrURL);
        player.OpenMedia(path, false);
    }

    public void OnClickToMoteScale(string url)
    {
        mShadow.gameObject.SetActive(true);
        mShadowReplayButton.gameObject.SetActive(false);

        mShadowPlayer.Events.AddListener(OnMediaPlayerEvent);
        mShadowReplayButton.OnClickAsObservable().Subscribe(v =>
        {
            Debug.Log("OnClickToMoteScale OnClickAsObservable");
            mShadowPlayer.Rewind(true);
            mShadowPlayer.Play();
        }).AddTo(this);
        MediaPath path = new MediaPath(url, MediaPathType.AbsolutePathOrURL);
        mShadowPlayer.OpenMedia(path, true);
    }

    public void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                Debug.Log("Player ReadyToPlay触发");
                break;
            case MediaPlayerEvent.EventType.Started:
                Debug.Log("Player开始事件触发");
               
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                Debug.Log("Player结束事件触发");
                mShadowReplayButton.gameObject.SetActive(showRepeat);
                mShadowPlayer.Control.Stop();
                break;
        }
    }
}
