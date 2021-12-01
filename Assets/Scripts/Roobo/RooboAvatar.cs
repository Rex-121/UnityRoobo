using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooboAvatar : MonoBehaviour
{


    public Animator animator;


    public void Speak()
    {
        animator.SetTrigger("Hello");

        //animator.SetBool("idle", false);
    }


    public void Idle()
    {

        animator.SetTrigger("Idle");

    }
}
