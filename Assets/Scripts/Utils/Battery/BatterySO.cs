using UnityEngine;

using Sirenix.OdinInspector;

public class BatterySO : ScriptableObject
{

    [PreviewField(50, ObjectFieldAlignment.Left), LabelText("电池背景")]
    public Sprite background;


    [PreviewField(50, ObjectFieldAlignment.Left), LabelText("充电中")]
    public Sprite charging;


    [PreviewField(50, ObjectFieldAlignment.Left), LabelText("电池条")]
    public Sprite inside;

    [LabelText("低电量阈值"), Range(0, 1)]
    public float lowBattery = 0.3f;

    [LabelText("低电量颜色")]
    public Color lowBatteryColor = Color.red;


    public Color BatterySliderColor(float level)
    {
        return (level > lowBattery) ? Color.white : lowBatteryColor;
    }

}
