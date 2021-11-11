using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using UniRx;

public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{

    [SerializeField]
    private GameObject creditPanel;

    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {

        Logging.Log("得分: " + credit.score);

        creditPanel.SetActive(true);

        Credit.Instance.PlayCreditOnScreen(credit, () =>
        {

            endPlay();
            creditPanel.SetActive(false);

        });
    }



    //private void Start()
    //{
    //    UniRx.Observable.Timer(System.TimeSpan.FromSeconds(2)).Subscribe(_ =>
    //    {
    //        StartCoroutine(GetR());
    //    });
        
    //}

    //IEnumerator GetR()
    //{


    //    Stopwatch sws = new Stopwatch();
    //    sws.Start();
        
    //    var a = Resources.Load("RatingStars");

    //    yield return Instantiate(a, creditPanel.transform);

    //    sws.Stop();
    //    UnityEngine.Debug.Log(string.Format("total: {0} ms", sws.ElapsedMilliseconds));
    //}


}
