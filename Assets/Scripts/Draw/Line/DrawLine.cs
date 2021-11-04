using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{


    public LineRenderer line;


    public void DrawTheLine(Vector3 from, Vector3 to)
    {
        line.positionCount = 2;

        line.SetPosition(0, from);

        line.SetPosition(1, to);
    }


    public void ClearLine()
    {
        line.positionCount = 0;
    }

}
