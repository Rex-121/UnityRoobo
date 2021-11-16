using System;
using UnityEngine;
using UniRx;

public class CoursewareCredit : MonoBehaviour, CoursewareCreditProtocol
{

    [SerializeField]
    private GameObject creditPanel;

    [SerializeField]
    private Canvas _canvas;


    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {

        Logging.Log("得分: " + credit.score);

        creditPanel.SetActive(true);

        _canvas.gameObject.SetActive(true);

        Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            endPlay();
            creditPanel.SetActive(false);
            _canvas.gameObject.SetActive(false);
        }).AddTo(this);
    }

}
