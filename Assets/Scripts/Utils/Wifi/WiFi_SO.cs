using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "WiFi信号")]
public class WiFi_SO : SerializedScriptableObject
{

    public Dictionary<int, Sprite> images = new Dictionary<int, Sprite>();


    public Sprite imageByStrength(int s)
    {
        return images[Mathf.Min(3, s)];
    }
}
