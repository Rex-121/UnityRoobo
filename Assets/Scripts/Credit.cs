using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "评分画布", menuName = "单例SO/评分画布")]
public class Credit : SingletonSO<Credit>, CoursewareCreditProtocol
{

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject ratingPrefab;

    private Canvas _canvas;


    private Canvas canvas
    {
        get
        {
            if (_canvas != null)
            {
                return _canvas;
            }

            var gb = Instantiate(prefab);

            _canvas = gb.GetComponent<Canvas>();

            return _canvas;

        }
    }

    public Canvas Init()
    {
        canvas.gameObject.SetActive(false);
        return canvas;
    }

    public void PlayCreditOnScreen(CreditData credit, Action endPlay)
    {


        canvas.gameObject.SetActive(true);

        var gb = Instantiate(ratingPrefab);

        gb.transform.SetParent(canvas.transform);

        Delay.Instance.DelayToCall(3, () =>
        {
            canvas.gameObject.SetActive(false);
            Destroy(gb);
            endPlay();
        });
    }
}
