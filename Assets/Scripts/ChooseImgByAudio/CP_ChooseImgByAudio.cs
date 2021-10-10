using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

public class CP_ChooseImgByAudio : CoursewarePlayer
{

    [LabelText("模版")]
    public Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioStruct> templates = new Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioStruct>();

}

public struct ChooseImgByAudioStruct
{
    [PreviewField(50, ObjectFieldAlignment.Left), PropertyOrder(1)]
    public Sprite background;

    [LabelText("状态显示图片SO")]
    public RWItemSprite_SO rw_so;

}

public enum ChooseImgByAudioTemplate
{
    Farm
}