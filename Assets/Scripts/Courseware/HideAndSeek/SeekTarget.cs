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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        contentPlayer = GetComponent<ContentPlayer>();
    }

    public void setAudio(string audioUrl) { this.audioUrl = audioUrl; }

    public void setOnEnd(Action onEnd) { this.onEnd = onEnd; }

    private void OnMouseDown()
    {
        if (isSeeked) { return; }
        isSeeked = true;
        audioSource.Play();
        Observable
            .Timer(TimeSpan.FromSeconds(audioSource.clip.length))
            .Subscribe((t)=> {
                contentPlayer.PlayContentByType(audioUrl, "audio");
                contentPlayer.status.Subscribe((status)=>{
                    if (status == PlayerEvent.finish) {
                        if (null != onEnd) {
                            onEnd();
                        }
                    }
                });
            });     
    }
}
