using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class RealmSecondaryEntrance : MonoBehaviour
{


    public void 选择延时课()
    {
        Navigation.Shared.选择延时课();
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
