using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UniRx;

public enum PuddingAction
{
    idle, hello, unhappy, bubble, starHappy, starMagic, starSpeak
}

[RequireComponent(typeof(ContentPlayer))]
public class Pudding : MonoBehaviour
{

    ContentPlayer player;
    Animator animator;

    public ReactiveProperty<PlayerEvent> speakStatus
    {
        get { return player.status; }
    }

    private void Awake()
    {
        player = GetComponent<ContentPlayer>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.status.Subscribe(status =>
        {
            switch (status)
            {
                case PlayerEvent.playing:
                case PlayerEvent.resume:
                    animator.SetBool("speak", true);
                    break;
                default:
                    animator.SetBool("speak", false);
                    break;
            }
            speakStatus.Value = status;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Do(PuddingAction action)
    {
        animator.SetTrigger(action.ToString());
    }

    public void DoSpeak()
    {
        animator.SetBool("speak", true);
    }

    public void DoStop()
    {
        animator.SetBool("speak", false);
    }

    public void Speak(string content, string type)
    {
        player.PlayContentByType(content, type);
    }

    public void StopSpeaking()
    {
        player.Stop();
    }

    //public void DoSth()
    //{
    //    Do(PuddingAction.starMagic);
    //}

    //public void SpeakSth()
    //{
    //    Speak("https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-12-08/c6o7tnt7heu83afi10eg.wav", "audio");
    //}
}
