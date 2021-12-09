using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using Sirenix.OdinInspector;

using UnityEngine.UI;

public class RealmBackButton : MonoBehaviour
{

    [ReadOnly]
    public Button backButton;

    private void Awake()
    {
        backButton = GetComponent<Button>();
    }
    void Start()
    {
        Navigation.Shared.menu.Select(v => v != Navigation.Menu.index).Subscribe(display =>
        {
            backButton.gameObject.SetActive(display);
        }).AddTo(this);

        backButton.OnClickAsObservable().Subscribe(_ => BackToIndex());

    }



    public void BackToIndex()
    {
        if (Navigation.Shared.classCategory != null)
        {
            Navigation.Shared.classCategory = null;
            Navigation.Shared.选择菜单(Navigation.Menu.secondary);

        }
        else
        {
            Navigation.Shared.切换学科(null);
            Navigation.Shared.选择菜单(Navigation.Menu.index);
        }

    }
}
