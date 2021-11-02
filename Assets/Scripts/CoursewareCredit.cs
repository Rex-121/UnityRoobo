using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{



    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {

        Debug.Log("得分: " + credit.score);

        Credit.Instance.PlayCreditOnScreen(credit, endPlay);
    }

}
