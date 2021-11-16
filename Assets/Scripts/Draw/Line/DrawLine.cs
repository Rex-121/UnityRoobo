using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    //public Line line;

    private void Awake()
    {
        //line = GetComponent<Line>();
    }

    public void DrawTheLine(Transform from, Transform to)
    {
        //Shapes.Draw.Line(Vector3.zero, transform.InverseTransformPoint(to.position), Color.black);

        //GetComponent<MeshFilter>().mesh.enab

        //line.End = transform.InverseTransformPoint(to.position);
    }


    public void ClearLine()
    {
        //line.Start = Vector3.zero;
        //line.End = Vector3.zero;
        //line.positionCount = 0;
    }

}
