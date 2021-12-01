using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

[RequireComponent(typeof(Toggle))]
public class HideImageWithToggle : MonoBehaviour
{

    Toggle toggle;

    public GameObject[] imageNeedDisappear;

    public GameObject[] imageNeedAppear;

    private void Start()
    {
        toggle.OnValueChangedAsObservable().Subscribe(v =>
        {
            AppearAll(imageNeedAppear, v);
            AppearAll(imageNeedDisappear, !v);
        });
    }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();


    }


    public void AppearAll(GameObject[] objects, bool appear)
    {
        foreach (var gb in objects)
        {
            gb.SetActive(appear);
        }

    }

}
