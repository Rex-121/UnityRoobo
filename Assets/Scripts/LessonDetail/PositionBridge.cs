using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBridge :MonoBehaviour
{
    private float right;

    public void setRight(float right)
    {
        this.right = right;
    }
    public float getRight()
    {
        return right;
    }
}
