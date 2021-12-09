using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 音频播放控制
/// </summary>
public class AudioControlScript : MonoBehaviour
{

    [LabelText("播放暂停(Image)")]
    public Image mPlayPauseImage;

    [LabelText("循环播放(Image)")]
    public Image mSingleLoopImage;

    [LabelText("音频播放")]
    public ContentPlayer mContentPlayer;

    [LabelText("播放图片")]
    public Sprite mPlaySprite;
    [LabelText("暂停图片")]
    public Sprite mStopSprite;

    [LabelText("循环图")]
    public Sprite mLoopSprite;
    [LabelText("非循环图")]
    public Sprite mUnLoopSprite;

    private bool mAutoPlay = false;

    private bool mIsLoop = false;

    private CW_Freeze_SO.FreezeEntity.Audio mAudio;

    /// <summary>
    /// 初始化音频播放信息
    /// </summary>
    /// <param name="freezeEntity"></param>
    public void InitAudioAndPlayType(CW_Freeze_SO.FreezeEntity freezeEntity)
    {
        var showHidePlay = freezeEntity.isRepeat;
        mPlayPauseImage.gameObject.SetActive(showHidePlay);

        mAudio = freezeEntity.audio;
        mAutoPlay = freezeEntity.isAuto;
        mIsLoop = freezeEntity.isLoop;

        InitPlayPauseImage(mAutoPlay);
        InitIsLoopImage(mIsLoop);
        if (mAutoPlay)
        {
            Debug.Log("AudioControlScript mAutoPlay");
            mContentPlayer.PlayContentByType(mAudio.content, mAudio.type.ToString());
        }
        Debug.Log("AudioControlScript player status =" + mContentPlayer.status.Value);
        mContentPlayer.status.Skip(1).Subscribe(v => {
            Debug.Log("AudioControlScript Subscribe=" + v);
            bool isPlay = false;
            switch (v)
            {
                case PlayerEvent.def:
                    isPlay = false;
                    break;
                case PlayerEvent.playing:
                    isPlay = true;
                    break;
                case PlayerEvent.pause:
                    isPlay = false;
                    break;
                case PlayerEvent.resume:
                    isPlay = true;
                    break;
                case PlayerEvent.stop:
                    break;
                case PlayerEvent.interrupt:
                    break;
                case PlayerEvent.finish:
                    isPlay = false;
                    if (mIsLoop)
                    {
                        mContentPlayer.Resume();
                    }
                    break;
                case PlayerEvent.none:
                    isPlay = false;
                    break;
            }
            if (!mIsLoop)
            {
                InitPlayPauseImage(isPlay);
            }
            else
            {
                InitPlayPauseImage(true);
            }
        }).AddTo(this);
    }


    /// <summary>
    /// 显示播放暂停图
    /// </summary>
    /// <param name="isPlay"></param>
    private void InitPlayPauseImage(bool isPlay)
    {
        if (isPlay)
        {
            mPlayPauseImage.sprite = mStopSprite;
        }
        else
        {
            mPlayPauseImage.sprite = mPlaySprite;
        }
    }

    /// <summary>
    /// 显示循环播放图
    /// </summary>
    /// <param name="isLoop"></param>
    private void InitIsLoopImage(bool isLoop)
    {
        if (isLoop)
        {
            mSingleLoopImage.sprite = mLoopSprite;
        }
        else
        {
            mSingleLoopImage.sprite = mUnLoopSprite;
        }
    }

    /// <summary>
    /// 播放，暂停点击
    /// </summary>
    public void OnPlayPauseClick()
    {
        Debug.Log("AudioControlScript OnPlayPauseClick=" + mContentPlayer.status.Value);
        var v = mContentPlayer.status.Value;
        switch (v)
        {
            case PlayerEvent.def:
                mContentPlayer.PlayContentByType(mAudio.content, mAudio.type.ToString());
                break;
            case PlayerEvent.loading:
            case PlayerEvent.playing:
            case PlayerEvent.resume:
                mContentPlayer.Pause();
                break;
            case PlayerEvent.pause:
                mContentPlayer.Resume();
                break;
            case PlayerEvent.stop:
                break;
            case PlayerEvent.interrupt:
                break;
            case PlayerEvent.finish:
                mContentPlayer.Resume();
                break;
            case PlayerEvent.none:
                break;
        }
    }

    /// <summary>
    /// 循环、取消循环播放点击
    /// </summary>
    public void OnLoopSwitchClick()
    {
        mIsLoop = !mIsLoop;
        InitIsLoopImage(mIsLoop);
    }
}
