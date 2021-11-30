using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
public class RealmSencondary : MonoBehaviour
{

    [ShowInInspector]
    private Transform canvas;

    public RealmSecondary_SO gbSO;

    private GameObject gb;



    void Start()
    {

        Observable.CombineLatest(Navigation.Shared.classType, Navigation.Shared.menu.Where(v => v == Navigation.菜单.二级), (v, _) => v)
            .Subscribe((type) =>
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
}
