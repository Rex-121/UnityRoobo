using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{

    [SerializeField]
    private GameObject creditPanel;

    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {

        Debug.Log("得分: " + credit.score);

        creditPanel.SetActive(true);

        Credit.Instance.PlayCreditOnScreen(credit, () =>
        {

            endPlay();
            creditPanel.SetActive(false);

        });
    }

}
