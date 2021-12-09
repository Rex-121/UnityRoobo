using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 图片选择题
/// </summary>
public class CW_PictureBoardChoice : CoursewarePlayer
{

    [LabelText("选项内容Grid")]
    public GridLayoutGroup mContentGrid;

    [LabelText("内容选项")]
    public GameObject mContentPrafeb;

    [LabelText("播放器")]
    public ContentPlayer mContentPlayer;


    public CW_PictureChoice_Carried_SO mCarrierImage;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="entity"></param>
    public void InitGridAndData(CW_PictrueBoardChoice_SO.PictureBoardEntity entity)
    {
        mContentPlayer.PlayContentByType(entity.question.content, entity.question.type);
        InitContentGrid(entity);
    }

    private void InitContentGrid(CW_PictrueBoardChoice_SO.PictureBoardEntity entity)
    {
        entity.options.ForEach(a=> {
            var contentObject = Instantiate(mContentPrafeb, mContentGrid.transform);
            SingleContentScript singleContent=contentObject.GetComponent<SingleContentScript>();
            singleContent.InitSingleOptions(a, entity.style, new System.Action<bool>(result=> {
                if (result)
                {
                    mContentPlayer.PlayContentByType(entity.rightFeedback.content, entity.rightFeedback.type);
                }
                else
                {
                    mContentPlayer.PlayContentByType(entity.errorFeedback.content, entity.errorFeedback.type);
                }
            }) );
            InitGridSpacing(entity);
        });
    }

    /// <summary>
    /// grid间距
    /// </summary>
    private void InitGridSpacing(CW_PictrueBoardChoice_SO.PictureBoardEntity entity)
    {
        var dic = mCarrierImage.dic[entity.style];
        mContentGrid.spacing = new Vector2(dic.dic[entity.options.Count], 0);
    }
}
