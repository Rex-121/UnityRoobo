using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UniRx;
using System.Diagnostics;

using UniRx;

public class AudioDownload : MonoBehaviour
{

    [ShowInInspector]
    string uri = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-09-30/c5anlfnfnhdbg6qujeo0.wav";


    [ShowInInspector]
    AudioClip clip;

    AudioSource audioSource;


    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();


        Observable.Timer(System.TimeSpan.FromSeconds(25)).Subscribe
            (_ =>
            {

                Storage.GetImage(Parcel.FromLocal("abc.png")).Take(1).Subscribe(v =>
                {
                    
      
                    spriteRenderer.sprite = v;

                }, (e) =>
                {
                    Logging.Log("error" + e);
                }).AddTo(this);


                Stopwatch sw = new Stopwatch();

                sw.Start();

                Storage.GetAudio(Parcel.FromLocal("abc.wav")).Take(1).Subscribe(v =>
                {

                    sw.Stop();

                    Logging.Log(sw.ElapsedMilliseconds);
                    SetClip(v);

                }, (e) =>
                {
                    Logging.Log("error" + e);
                }).AddTo(this);


            }).AddTo(this);


    }


    void SetClip(AudioClip clip)
    {

        Logging.Log("name" + clip.name);

        audioSource.clip = clip;
        audioSource.Play();
    }

}
