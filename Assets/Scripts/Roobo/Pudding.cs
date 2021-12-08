using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UniRx;

public enum PuddingAction
{
    idle, hello, speak, unhappy, bubble, starHappy, starMagic, starSpeak
}


public class Pudding : MonoBehaviour
{

    ContentPlayer player;
    Animator animator;

    public ReactiveProperty<PlayerEvent> speakStatus
    {
        get { return player.status; }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<ContentPlayer>();
        animator = GetComponent<Animator>();

        player.status.Subscribe(status =>
        {
            switch (status)
            {

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

    public void Speak(string content, string type)
    {
        player.PlayContentByType(content, type);
    }
 
    
}
