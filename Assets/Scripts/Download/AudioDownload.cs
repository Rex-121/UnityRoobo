using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UniRx;
using System;

public class AudioDownload : MonoBehaviour
{

    [ShowInInspector]
    string uri = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-09-30/c5anlfnfnhdbg6qujeo0.wav";


    [ShowInInspector]
    AudioClip clip;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        HttpRx.GetAudio(uri).Take(1).Subscribe(v =>
        {
            SetClip(v);
        }).AddTo(this);
    }


    void SetClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

}
