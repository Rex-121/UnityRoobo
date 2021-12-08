using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

[RequireComponent(typeof(Text))]
public class PoemTextController : MonoBehaviour
{
    private PoemManager.PoemBean poemBean;
    private Text text;
    private BehaviorSubject<PoemManager.PoemBean> stream;

    public void setup(PoemManager.PoemBean poemBean, BehaviorSubject<PoemManager.PoemBean> stream)
    {
        this.poemBean = poemBean;
        this.stream = stream;
        if (null == text) {
            initText();
        }
        text.text = poemBean.text;
        stream.Subscribe((result) =>
        {
            if (result == null)
            {
                changeStatus(PoemTextStatus.NORMAL);
                return;
            }
            if (result.id == poemBean.id)
            {
                changeStatus(result.poemTextStatus);
            }
            else
            {
                changeStatus(PoemTextStatus.NORMAL);
            }
        }).AddTo(this);
    }

    private void changeStatus(PoemTextStatus status)
    {
        switch (status)
        {
            case PoemTextStatus.NORMAL:
                text.color = Color.black;
                break;
            case PoemTextStatus.HIGHTLIGHT:
                text.color = Color.red;
                break;
            case PoemTextStatus.LOWLIGHT:
                text.color = Color.gray;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initText();  
    }

    private void initText()
    {
        text = GetComponent<Text>();
    }
}
