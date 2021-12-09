using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ContentPlayer))]
public class SeekTarget : MonoBehaviour
{
    private string audioUrl;
    private bool isSeeked = false;
    private AudioSource audioSource;
    private ContentPlayer contentPlayer;
    private Action onEnd;
    public GameObject starBomb;
    public float effectsScaler = 0.07f;
    public float effectsMinScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        contentPlayer = GetComponent<ContentPlayer>();
    }

    public void setAudio(string audioUrl) { this.audioUrl = audioUrl; }

    public void setOnEnd(Action onEnd) { this.onEnd = onEnd; }

    private float getStarBombScale()
    {
        return Math.Max(effectsScaler * (transform.localScale.x + transform.localScale.y) / 2, effectsMinScale);
    }

    private void OnMouseDown()
    {
        if (isSeeked) { return; }
        isSeeked = true;
        audioSource.Play();
        if (null != starBomb)
        {
            starBomb.SetActive(true);
            starBomb.transform.localScale = new Vector3(getStarBombScale(), getStarBombScale(), 1);
        }
        //Observable
        //    .Timer(TimeSpan.FromSeconds(audioSource.clip.length))
        //    .Subscribe((t) =>
        //    {
        //        contentPlayer.PlayContentByType(audioUrl, "audio");
        //        contentPlayer.status.Subscribe((status) =>
        //        {
        //            if (status == PlayerEvent.finish)
        //            {
        //                if (null != onEnd)
        //                {
        //                    onEnd();
        //                }
        //            }
        //        });
        //    });
        Observable
          .Timer(TimeSpan.FromSeconds(audioSource.clip.length))
          .Select(_ => { contentPlayer.PlayContentByType(audioUrl, "audio"); return 0; })
          .ContinueWith(contentPlayer.status)
          .Subscribe((t) =>
          {
              if (t == PlayerEvent.finish)
              {
                  if (null != onEnd)
                  {
                      onEnd();
                  }
              }
          }).AddTo(this);
    }
}
