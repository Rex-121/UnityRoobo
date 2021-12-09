using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

public enum MicrophoneState
{
    ENABLE,
    DISABLE,
    RECORDING,
    HIDE,
}

[RequireComponent(typeof(SpriteRenderer))]
public class MicrophoneController : MonoBehaviour
{
    [Required]
    public Sprite disabledSprite, enabledSprite;
    public float recordDuration = 3f;
    private SpriteRenderer spriteRenderer;
    [Required]
    public SpriteRenderer progressRenderer;
    private BehaviorSubject<MicrophoneState> stateStream;
    private Tween progressTween;
    public GameObject starBombPrefab;
    private Action onRecordingEnd;

    public void setStateStream(BehaviorSubject<MicrophoneState> stateStream)
    {
        this.stateStream = stateStream;

        this.stateStream.Subscribe(state =>
        {
            switch (state)
            {
                case MicrophoneState.ENABLE:
                    spriteRenderer.enabled = true;
                    progressRenderer.enabled = false;
                    spriteRenderer.sprite = enabledSprite;
                    progressTween?.Kill();
                    break;
                case MicrophoneState.DISABLE:
                    spriteRenderer.enabled = true;
                    progressRenderer.enabled = false;
                    spriteRenderer.sprite = disabledSprite;
                    progressTween?.Kill();
                    break;
                case MicrophoneState.RECORDING:
                    progressRenderer.enabled = true;
                    progressTween.Kill();
                    progressRenderer.material.SetFloat("_progress", 0);
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = enabledSprite;
                    startRecording();
                    break;
                case MicrophoneState.HIDE:
                    spriteRenderer.enabled = false;
                    progressRenderer.enabled = false;
                    progressTween?.Kill();
                    break;
            }
        }).AddTo(this);
    }

    public void setOnRecordingEnd(Action onRecordingEnd) { this.onRecordingEnd = onRecordingEnd; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void startRecording()
    {
        progressTween = DOTween.To(() => progressRenderer.material.GetFloat("_progress"),
              x => progressRenderer.material.SetFloat("_progress", x), 1, recordDuration)
              .SetEase(Ease.Linear)
              .OnComplete(() =>
              {
                  if (null != onRecordingEnd)
                  {
                      onRecordingEnd();
                  }
                  bomb();
                  if (null != stateStream)
                  {
                      stateStream.OnNext(MicrophoneState.DISABLE);
                  }
              });
    }

    private void bomb()
    {
        if (null != starBombPrefab)
        {
            GameObject starBomb = Instantiate(starBombPrefab, transform);
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe((l)=> {
                Destroy(starBomb);
            });
        }
    }
}
