using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

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

    private System.IDisposable mSingleDisposable;

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
            singleContent.InitSingleOptions(a, entity.style, new System.Action<CW_PictrueBoardChoice_SO.PictureBoardEntity.Option>(option=> {
                if (mSingleDisposable != null)
                {
                    mSingleDisposable.Dispose();
                }
                if (string.IsNullOrEmpty(option.audio))
                {
                    var result = option.isAnswer == 1;
                    if (result)
                    {
                        if(entity.rightFeedback!=null)
                        {
                            mContentPlayer.PlayContentByType(entity.rightFeedback.content, entity.rightFeedback.type);
                        }
                    }
                    else
                    {
                        if (entity.errorFeedback != null)
                        {
                            mContentPlayer.PlayContentByType(entity.errorFeedback.content, entity.errorFeedback.type);
                        }
                    }
                    mSingleDisposable = mContentPlayer.status.Skip(1).Subscribe(status => {
                        if (status == PlayerEvent.finish)
                        {
                            var result = option.isAnswer == 1;
                            if (result)
                            {
                                Debug.Log("CW_PictureBoardChoice  PlayerEvent.finish");
                                creditDelegate.PlayCreditOnScreen(new Score(), () => {
                                    Debug.Log("CW_PictureBoardChoice  PlayCreditOnScreen");
                                    DidEndCourseware(this);
                                });
                            }
                        }

                    }).AddTo(this);
                }
                else
                {
                    //播放对应音频
                    mContentPlayer.PlayURL(option.audio);
                    //监听状态播放反馈结果
                    mSingleDisposable = mContentPlayer.status.Skip(1).Subscribe(status => {
                        if (status == PlayerEvent.finish)
                        {
                            var result = option.isAnswer == 1;
                            if (result)
                            {
                                if (entity.rightFeedback != null)
                                {
                                    mContentPlayer.PlayContentByType(entity.rightFeedback.content, entity.rightFeedback.type);
                                }
                            }
                            else
                            {
                                if (entity.errorFeedback != null)
                                {
                                    mContentPlayer.PlayContentByType(entity.errorFeedback.content, entity.errorFeedback.type);
                                }
                            }
                            mSingleDisposable = mContentPlayer.status.Skip(1).Subscribe(status => {
                                if (status == PlayerEvent.finish)
                                {
                                    var result = option.isAnswer == 1;
                                    if (result)
                                    {
                                        Debug.Log("CW_PictureBoardChoice  PlayerEvent.finish");
                                        creditDelegate.PlayCreditOnScreen(new Score(), () => {
                                            Debug.Log("CW_PictureBoardChoice  PlayCreditOnScreen");
                                            DidEndCourseware(this);
                                        });
                                    }
                                }

                            }).AddTo(this);

                        }

                    }).AddTo(this);
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
