using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class InLineDefaultPosition_SO : ScriptableObject
{
    [LabelText("物体缩放，默认 `1` ")]
    public float scale = 1;

    public List<Vector2> positions;

}


