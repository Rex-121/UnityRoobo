using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{



    public void PlayCreditOnScreen(Credit credit, Action endPlay)
    {

        Debug.Log("得分: " + credit.score);

        Delay.Instance.DelayToCall(3, endPlay);
    }

}
