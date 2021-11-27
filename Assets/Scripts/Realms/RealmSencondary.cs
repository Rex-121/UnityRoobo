using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;

public class RealmSencondary : MonoBehaviour
{

    [ShowInInspector]
    private Transform canvas;

    public RealmSecondary_SO gbSO;

    private GameObject gb;

    // Start is called before the first frame update
    void Start()
    {
        Navigation.Shared.classType.Subscribe(type =>
        {
            if (type == null) return;
            canvas = transform.Find("二级入口展示");
            gb = Instantiate(gbSO.GetSecondary((学科.类型)type), canvas);
        }).AddTo(this);
    }


    private void OnDisable()
    {

        if (gb == null) return;

        Destroy(gb);
    }


    public void BackToIndex()
    {
        Navigation.Shared.切换学科(null);
    }
}
