using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IScrollLimiter : MonoBehaviour
{
    //以下方法需要减去摄像机宽高
    abstract public float getLeftLimit();
    abstract public float getRightLimit();
    abstract public float getTopLimit();
    abstract public float getBottomLimit();
}
