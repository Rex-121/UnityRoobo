using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class RealmSecondaryEntrance : MonoBehaviour
{


    public void 选择延时课()
    {
        Navigation.Shared.选择延时课();
        Navigation.Shared.选择菜单(Navigation.Menu.third);
    }


    public void 选择人文学科()
    {
        Navigation.Shared.切换学科(ClassSubjectType.Chinese);
        Navigation.Shared.选择延时课();
        Navigation.Shared.选择菜单(Navigation.Menu.third);
    }

    public void 选择英语学科()
    {
        Navigation.Shared.切换学科(ClassSubjectType.English);
        Navigation.Shared.选择延时课();
        Navigation.Shared.选择菜单(Navigation.Menu.third);
    }

    [LabelText("横幅")]
    public GameObject bannerPrefab;

    [Serializable]
    public struct Banner
    {
        public Transform position;

        public Sprite title;
    }


    [ShowInInspector]
    public Banner[] banners;// = new Banner[] { };


    private void Start()
    {
        foreach (var banner in banners)
        {
            var b = Instantiate(bannerPrefab, banner.position);

            b.GetComponent<RealmSecondary_Banner>().SetTitle(banner.title);
        }
    }

}
