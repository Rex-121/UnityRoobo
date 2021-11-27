using UnityEngine;

using UniRx;

using System;
using UnityEngine.UI;

public class Wifi : MonoBehaviour
{

    public WiFi_SO wifiSO;

    public Image image;

    void Start()
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(5))
            .Select(_ => NativeCalls.Shared.WifiStrength().strength)
            .DistinctUntilChanged()
            .Subscribe(v =>
        {
            image.sprite = wifiSO.imageByStrength(v);
        }).AddTo(this);
    }
}
