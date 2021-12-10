using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 定格图片带音频
/// </summary>
public class FreezeAudioImageView : MonoBehaviour
{
    [LabelText("顶部Grid")]
    public GridLayoutGroup mTopGrid;

    [ShowInInspector]
    [LabelText("底部Grid")]
    public GridLayoutGroup mBottomGrid;

    [LabelText("中部Grid")]
    public GridLayoutGroup mCenterGrid;

    [LabelText("遮罩")]
    public Transform mShadow;

    public GameObject prefabImage;

    private CW_Freeze_SO.FreezeEntity freezeEntity;

    /// <summary>
    /// 初始化显示内容
    /// </summary>
    /// <param name="imageUrls"></param>
    public void InitGrids(CW_Freeze_SO.FreezeEntity freezeEntity)
    {
        this.freezeEntity = freezeEntity;
        List<CW_Freeze_SO.FreezeEntity.AudioAndImage> audioAndImages = freezeEntity.imgList;
        Debug.Log("FreezeAudioImageView=>" + audioAndImages.Count);
        if (audioAndImages.Count > 0)
        {
            if (audioAndImages.Count <= 4)
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
            InitImages(audioAndImages);
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
    /// <param name="imageUrls"></param>
    private void InitImages(List<CW_Freeze_SO.FreezeEntity.AudioAndImage> audioAndImages)
    {
        Debug.Log("FreezeAudioImageView=InitImages>>" + audioAndImages.Count);
        if (audioAndImages.Count <= 4)
        {
            audioAndImages.ForEach(e =>
            {
                StartLoadImage(mCenterGrid, e);
            });
        }
        else
        {
            int counts = audioAndImages.Count;
            if (audioAndImages.Count > 8)
            {
                counts = 8;
            }

            int topCount = (counts + 1) / 2;

            List<CW_Freeze_SO.FreezeEntity.AudioAndImage> topUrls = audioAndImages.GetRange(0, topCount);

            topUrls.ForEach(e => {
                StartLoadImage(mTopGrid, e);
            });

            List<CW_Freeze_SO.FreezeEntity.AudioAndImage> buttomUrls = audioAndImages.GetRange(topCount, counts - topCount);
            buttomUrls.ForEach(e => {
                StartLoadImage(mBottomGrid, e);
            });

        }
    }


    /// <summary>
    /// 添加到grid ,并加载网络图片
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public void StartLoadImage(GridLayoutGroup grid, CW_Freeze_SO.FreezeEntity.AudioAndImage audioAndImage)
    {
        Debug.Log("StartLoadImage" + grid + " " + grid.name);

        GameObject prefab = Instantiate(prefabImage, grid.transform);
        prefab.GetComponent<ImageAspectScript>().onClickAudioImage += OnClickToMoteScale;
        prefab.GetComponent<ImageAspectScript>().andImage = audioAndImage;

        Image[] asps = prefab.GetComponentsInChildren<Image>();
        Debug.Log(" StartLoadImage image Size:" + asps.Length);

        Debug.Log("StartLoadImage image=" + audioAndImage.img);

        WebReqeust.GetTexture(audioAndImage.img, (texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            asps[0].sprite = tempSprite;

        }, (e) =>
        {
            Debug.Log("FreezeAudioImageView StartLoadImage error:" + e);
        });
    }

    public void OnClickToMoteScale(GameObject gb, CW_Freeze_SO.FreezeEntity.AudioAndImage audioAndImage)
    {
        var outerPrafeb = Instantiate(gb, mShadow.transform);
        outerPrafeb.GetComponent<Button>().enabled = false;
        ImageAspectScript imageAspect=outerPrafeb.GetComponent<ImageAspectScript>();
        //是否展示重复播放按钮

        outerPrafeb.GetComponent<RectTransform>().SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        mShadow.gameObject.SetActive(true);

        var aa = outerPrafeb.transform.DOScale(new Vector3(2, 2, 1000), 0.3f);
        aa.Complete();
        Debug.Log("OnClickToMoteScale = " + audioAndImage);

        if (audioAndImage != null && !string.IsNullOrEmpty(audioAndImage.audio))
        {
            //播放音频
            ContentPlayer player = mShadow.GetComponent<ContentPlayer>();
            player.PlayURL(audioAndImage.audio);
            player.status.Skip(1).Subscribe(v =>
            {
                if (v == PlayerEvent.finish)
                {
                    Debug.Log("OnClickToMoteScale player isRepeat=" + freezeEntity.isRepeat);
                    if (!imageAspect.mReplay.IsDestroyed())
                    {
                        imageAspect.mReplay.gameObject.SetActive(freezeEntity.isRepeat);
                    }
                    
                }
            });
            if (!imageAspect.mReplay.IsDestroyed())
            {
                imageAspect.mReplay.OnClickAsObservable().Subscribe(v =>
                {
                    Debug.Log("OnClickToMoteScale OnClickAsObservable");
                    player.PlayURL(audioAndImage.audio);
                }).AddTo(this);
            }

            
        }
        
    }
}
