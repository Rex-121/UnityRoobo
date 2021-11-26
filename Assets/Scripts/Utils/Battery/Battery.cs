using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using System;
using Sirenix.OdinInspector;

public class Battery : MonoBehaviour
{

    public BatterySO batterySO;

    public Image backgroundImage;


    public Image batteryImage;

    public bool displayLevel = false;

    public Text levetText;

    private void Start()
    {

        ChangeBackGroundImageIfNeeded();
    }

    void ChangeBackGroundImageIfNeeded()
    {
        var batteryStatus = Observable.EveryUpdate()
            .Select((a) => isCharging())
            .DistinctUntilChanged();

        batteryStatus.Subscribe(v => Logging.Log("充电中 " + v)).AddTo(this);

        /// 切换充电状态图片
        batteryStatus
            .Select((charing) => charing ? batterySO.charging : batterySO.background)
            .Subscribe(sprite => backgroundImage.sprite = sprite)
            .AddTo(this);

        /// 隐藏/展示 电池条
        batteryStatus.Subscribe(ToggleBatteyCharging).AddTo(this);

        /// 改变电池电量条
        batteryStatus.Where(c => !c).Select((_) => SystemInfo.batteryLevel)
            .DistinctUntilChanged()
            .Subscribe(ChangeLevel)
            .AddTo(this);

    }


    void ToggleBatteyCharging(bool charging)
    {
        batteryImage.gameObject.SetActive(!charging);
        ChangeLevel(SystemInfo.batteryLevel);
    }

    void ChangeLevel(float level) {

        batteryImage.GetComponent<RectTransform>().sizeDelta = new Vector2(42 * level, 20);

        batteryImage.color = batterySO.BatterySliderColor(level);

        if (displayLevel && levetText != null)
        {
            levetText.text = (level * 100).ToString("0") + "%";
        }
    }



    bool isCharging()
    {
        switch (SystemInfo.batteryStatus)
        {
            case BatteryStatus.Charging:
            case BatteryStatus.Full:
                return true;
            default:
                return false;
        }
    }
}
