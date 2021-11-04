using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IScrollLimiter : MonoBehaviour
{
    abstract public float getLeftLimit();
    abstract public float getRightLimit();
    abstract public float getTopLimit();
    abstract public float getBottomLimit();
}
