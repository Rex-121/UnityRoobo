using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 单个选项
/// </summary>
public class SingleContentScript : MonoBehaviour
{
    [LabelText("图片内容")]
    public Image mImageContent;

    public CW_PictureChoice_Carried_SO mCarrierImage;

    [LabelText("遮罩图片")]
    public Image mImageMask;

    [LabelText("载体图片")]
    public Image mImageCarrier;

    [LabelText("图片边框")]
    public Image mImageBorder;

    [LabelText("结果反馈图")]
    public Image mImageResult;

    [LabelText("正确图片")]
    public Sprite rightImage;

    [LabelText("错误图片")]
    public Sprite wrongImage;



    private CW_PictrueBoardChoice_SO.PictureBoardEntity.Option option;

    private System.Action<bool> resultAction;

    /// <summary>
    /// 初始化单个内容
    /// </summary>
    public void InitSingleOptions(CW_PictrueBoardChoice_SO.PictureBoardEntity.Option option, 
        CW_PictrueBoardChoice_SO.PictureBoardEntity.QuestionStyle style, 
        System.Action<bool> resultAction)
    {
        this.option = option;
        this.resultAction = resultAction;

        if (option.isAnswer == 1)
        {
            mImageResult.sprite = rightImage;
        }
        else
        {
            mImageResult.sprite = wrongImage;
        }
        WebReqeust.GetTexture(option.content, (texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            mImageContent.sprite = tempSprite;
        }, (e) =>
        {
            Debug.Log("InitSingleOptions load image error:" + e);
        });

        mImageCarrier.gameObject.SetActive(style != CW_PictrueBoardChoice_SO.PictureBoardEntity.QuestionStyle.playground);
        Debug.Log("QuestionStyle =>" + style);
        if (mCarrierImage.dic.ContainsKey(style)) {
            var dic = mCarrierImage.dic[style];
            InitMaskAndCarrier(dic);
        }
        
    }

    /// <summary>
    /// 点击
    /// </summary>
    public void OnChooseClick()
    {
        if (option != null)
        {
            resultAction(option.isAnswer == 1);
            mImageResult.gameObject.SetActive(true);
            
            Observable.Timer(System.TimeSpan.FromSeconds(1.5)).Subscribe(e=> {
                mImageResult.gameObject.SetActive(false);
            }).AddTo(this);
        }
    }

    /// <summary>
    /// 遮罩载体等信息
    /// </summary>
    /// <param name="config"></param>
    private void InitMaskAndCarrier(CW_PictureChoice_Carried_SO.Config config)
    {
        
        mImageMask.sprite = config.mask;
        mImageBorder.sprite = config.mask;
        mImageCarrier.sprite = config.carrier;
        mImageCarrier.SetNativeSize();
        mImageCarrier.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, config.carrierPosY);
    }
}
