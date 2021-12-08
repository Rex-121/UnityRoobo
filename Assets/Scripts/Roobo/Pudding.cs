using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum PuddingAction
{
    hello, speak, unhappy, bubble, starHappy, starMagic, starSpeak
}


public class Pudding : MonoBehaviour
{
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Do(PuddingAction action)
    {
        animator.SetTrigger(action.ToString());

    }
 

}
