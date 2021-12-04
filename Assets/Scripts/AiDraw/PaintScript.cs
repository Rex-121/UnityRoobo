using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

[RequireComponent(typeof(Toggle))]
public class PaintScript : MonoBehaviour
{

    private Tweener mTweenerChose;
    private Tweener mTweenerDisChose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DidToggleChange()
    {
        Debug.Log("enter-->DidToggleChange");
        bool v=GetComponent<Toggle>().isOn;
        Debug.Log("enter-->DidToggleChange="+v);
        if (v)
        {
            mTweenerChose = transform.DOLocalMoveY(50, 0.1f);
        }
        else
        {
            mTweenerDisChose = transform.DOLocalMoveY(0, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (mTweenerChose != null)
        {
            mTweenerChose.Kill();
        }
        if (mTweenerDisChose != null)
        {
            mTweenerDisChose.Kill();
        }
    }

}
