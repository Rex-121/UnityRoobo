using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class RWDefaultOption : RightWrongOptionAttachment
{

    public CoursewareDataBridge_SO dataBridge;


    private void OnEnable()
    {
        RegisterDataBridge();
    }

    private void OnDisable()
    {
        ResignDataBridge();
    }

    public void RegisterDataBridge()
    {

        dataBridge.didEndCourseware += DidEndCourseware;

    }

    public void ResignDataBridge()
    {

        dataBridge.didEndCourseware -= DidEndCourseware;

    }

    void DidEndCourseware(CoursewarePlayer player)
    {
        GetComponent<FlowMachine>().GetComponent<Variables>().declarations.Set("StateLocked", true);
    }
}
