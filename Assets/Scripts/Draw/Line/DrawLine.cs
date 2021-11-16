using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shapes;

public class DrawLine : MonoBehaviour
{

    public Line line;

    private void Awake()
    {
        line = GetComponent<Line>();
    }

    public void DrawTheLine(Transform from, Transform to)
    {
        line.End = transform.InverseTransformPoint(to.position);
    }


    public void ClearLine()
    {
        line.Start = Vector3.zero;
        line.End = Vector3.zero;
        //line.positionCount = 0;
    }

}
