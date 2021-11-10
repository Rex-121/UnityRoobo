using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UniRx;

public class CW_FollowRead_Canvas : MonoBehaviour
{


    [SerializeField]
    private GameObject textPre;

    [SerializeField]
    private Transform lineOne;


    [SerializeField]
    private RectTransform rows;

    public void MakeData(List<string> k)
    {
        k.ForEach((value) =>
        {
            var text = Instantiate(textPre, lineOne);
            text.name = value;
            text.GetComponent<Text>().text = value;
        });

        Observable.EveryUpdate()
            .Select(_ => rows.rect.width)
            .Where(v => v != 0)
            .TakeUntilDestroy(this)
            .Take(1)
            .Subscribe(v =>
        {
            rows.localPosition = new Vector2(v / 2, rows.localPosition.y);
        });
    }


}
