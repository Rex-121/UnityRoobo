using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using DG.Tweening;


public class CommonAlert : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text contentText;
    public Action<bool> callback;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void SetContent(string content)
    {
        contentText.text = content;
    }

    public void Present()
    {
        var tween = gameObject.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        var mask = transform.parent.GetComponent<DOTweenAnimation>();
        mask.DOPlayForward();
    }

    public void Dismiss(bool confirm)
    {
        var tween = gameObject.transform.DOScale(0.2f, 0.3f).SetEase(Ease.InBack);
        tween.onComplete = () =>
        {
            callback?.Invoke(confirm);
            Destroy(gameObject);
        };

        var mask = transform.parent.GetComponent<DOTweenAnimation>();
        mask.DOPlayBackwards();
    }
}
