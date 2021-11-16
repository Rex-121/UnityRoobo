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
        //line.positionCount = 2;
        //line.Start = from;

        var p = transform.position;
        line.End = transform.InverseTransformPoint(to.position);
        //line.End = new Vector3(to.position.x, -(p - to.position).magnitude, 0);
        Logging.Log(transform.localPosition);
        Logging.Log(p);
        Logging.Log(to.position);
        Logging.Log((p - new Vector3(0, to.position.y, 0)).magnitude);
        Logging.Log((p - to.position).magnitude);
        //line.SetPosition(0, from);


        Logging.Log(transform.InverseTransformPoint(to.position));

        //line.SetPosition(1, to);
    }


    public void ClearLine()
    {
        line.Start = Vector3.zero;
        line.End = Vector3.zero;
        //line.positionCount = 0;
    }

}
