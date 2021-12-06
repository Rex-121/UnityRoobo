using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CW_Freeze : CoursewarePlayer
{

    public GameObject prefabImage;
    public GameObject prefabVideo;

    [ShowInInspector]
    [LabelText("定格内容容器")]
    public GameObject freezeContainer;

    [LabelText("图片遮罩")]
    public Transform mShadow;

    [LabelText("视频遮罩")]
    public Transform mShadowVideo;

    [LabelText("下一环节")]
    public Image mNextStep;

    public void InitGridAndData(CW_Freeze_SO.FreezeEntity freezeEntity)
    {
        if (freezeEntity != null) {
            mNextStep.gameObject.SetActive(freezeEntity.isNext);
            switch (freezeEntity.type)
            {
                case CW_Freeze_SO.FreezeEntity.Type.audio:
                    break;
                case CW_Freeze_SO.FreezeEntity.Type.audioAndImage:
                    var gameObject = Instantiate(prefabImage, freezeContainer.transform);
                    var comp = gameObject.GetComponent<FreezeImageView>();
                    comp.mShadow = mShadow;
                    comp.InitGrids(freezeEntity.images);
                    break;
                case CW_Freeze_SO.FreezeEntity.Type.noDisplay:
                    break;
                case CW_Freeze_SO.FreezeEntity.Type.video:
                    var gameVideo = Instantiate(prefabVideo, freezeContainer.transform);
                    var videoView = gameVideo.GetComponent<FreezeVideoView>();
                    videoView.mShadow = mShadowVideo;
                    videoView.showRepeat = freezeEntity.isRepeat;
                    videoView.InitGrids(freezeEntity.videoList);
                    break;
            }
        }
    }

    /// <summary>
    /// 遮罩图片点击隐藏
    /// </summary>
    public void OnOffActiveShadow()
    {
        Debug.Log("OnOffActiveShadow =" + mShadow.childCount);
        for (int i = 0; i < mShadow.childCount; i++)
        {
            Destroy(mShadow.GetChild(i).gameObject);
        }
        mShadow.gameObject.SetActive(false);
    }

    /// <summary>
    /// 遮罩视频点击隐藏
    /// </summary>
    public void OnOffActiveVideoShadow()
    {
        Debug.Log("OnClickToMoteScale OnOffActiveVideoShadow");
        for (int i = 0; i < mShadowVideo.childCount; i++)
        {
            Destroy(mShadowVideo.GetChild(i).gameObject);
        }
        mShadowVideo.gameObject.SetActive(false);
    }
}
