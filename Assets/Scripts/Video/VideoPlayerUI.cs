using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UniRx;

public class VideoPlayerUI : MonoBehaviour
{
    [Required]
    public MediaPlayer mediaPlayer;
    [Required]
    public Slider progressSlider;
    private bool _wasPlayingBeforeTimelineDrag;
    private IDisposable disappearSchedule;
    private bool isDraging = false;
    private bool isControllUIShown = true;
    private float lastVideoClickTime;
    private int videoClickCount = 0;
    private IDisposable doubleClickTimer;

    private void Start()
    {
        CreateProgressDragEvents();
        scheduleDisappear();
    }

    private void CreateProgressDragEvents()
    {

        EventTrigger trigger = progressSlider.gameObject.GetComponent<EventTrigger>();
        if (trigger != null)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { OnProgressSliderBeginDrag(); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Drag;
            entry.callback.AddListener((data) => { OnProgressSliderDrag(); });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => { OnProgressSliderEndDrag(); });
            trigger.triggers.Add(entry);
        }
    }

    private void OnProgressSliderBeginDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            _wasPlayingBeforeTimelineDrag = mediaPlayer.Control.IsPlaying();
            if (_wasPlayingBeforeTimelineDrag)
            {
                mediaPlayer.Pause();
            }
            progressSlider.handleRect.localScale = new Vector3(2.5f, 2.5f, 1f);
            OnProgressSliderDrag();
        }
    }

    private void OnProgressSliderDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            TimeRange timelineRange = GetTimelineRange();
            double time = timelineRange.startTime + (progressSlider.value * timelineRange.duration);
            mediaPlayer.Control.Seek(time);
            isDraging = true;
        }
    }

    private void OnProgressSliderEndDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            if (_wasPlayingBeforeTimelineDrag)
            {
                mediaPlayer.Play();
                _wasPlayingBeforeTimelineDrag = false;
            }
            progressSlider.handleRect.localScale = new Vector3(1f, 1f, 1f);
            isDraging = false;
            scheduleDisappear();
        }
    }

    private void Update()
    {
        if (mediaPlayer.Info != null)
        {
            TimeRange timelineRange = GetTimelineRange();
            // Update time slider position
            if (progressSlider)
            {
                double t = 0.0;
                if (timelineRange.duration > 0.0)
                {
                    t = ((mediaPlayer.Control.GetCurrentTime() - timelineRange.startTime) / timelineRange.duration);
                }
                progressSlider.value = Mathf.Clamp01((float)t);
            }
        }
    }

    public void handleVideoTouch()
    {
        if (null != doubleClickTimer)
        {
            doubleClickTimer.Dispose();
        }
        videoClickCount++;
        doubleClickTimer = Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(V =>
        {
            videoClickCount = 0;
        }).AddTo(this);
        if (videoClickCount >= 2)
        {
            videoClickCount = 0;
            toggleVideoPlay();
        }
        else
        {
            toggleUIVisible();
        }
    }

    private void toggleVideoPlay()
    {
        if (mediaPlayer.Control.IsPlaying())
        {
            mediaPlayer.Pause();
        }
        else {
            mediaPlayer.Play();
        }
    }

    private void toggleUIVisible()
    {
        if (isControllUIShown)
        {
            hideControllUI();
        }
        else {
            showControllUI();
            scheduleDisappear();
        }
    }

    private void showControllUI()
    {
        isControllUIShown = true;
        progressSlider.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void hideControllUI()
    {
        isControllUIShown = false;
        progressSlider.transform.localScale = Vector3.zero;
    }

    private void scheduleDisappear()
    {
        if (null != disappearSchedule)
        {
            disappearSchedule.Dispose();
        }
        disappearSchedule = Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(v =>
        {
            if (isDraging) { return; }
            hideControllUI();
        });
    }

    private TimeRange GetTimelineRange()
    {
        if (mediaPlayer.Info != null)
        {
            return Helper.GetTimelineRange(mediaPlayer.Info.GetDuration(), mediaPlayer.Control.GetSeekableTimes());
        }
        return new TimeRange();
    }
}
