using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "音选图数据桥梁", menuName = "数据桥梁/音选图")]
public class CP_ChooseImgByAudioBridge_SO : CoursewareDataBridge_SO
{

    public UnityAction<RightWrongOptionAttachment> action;


    public void InvokeAction(RightWrongOptionAttachment rw)
    {
        action?.Invoke(rw);
    }

}
