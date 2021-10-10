using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

[RequireComponent(typeof(SpriteRenderer))]
public class CP_ChooseImgByAudio : CoursewarePlayer
{

    [LabelText("需要展示的模版")]
    public ChooseImgByAudioTemplate template;

    public CP_ChooseImgByAudioBridge_SO dataBridge;

    [LabelText("模版")]
    public Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioTemplateStruct> templates = new Dictionary<ChooseImgByAudioTemplate, ChooseImgByAudioTemplateStruct>();

    public void DidBeginCourseware()
    {

    }

    public void DidEndCourseware()
    {

    }


    private void Start()
    {

        dataBridge.action += Vvv;

        var item = templates[template];


        var length = 4;

        var positions = item.inLinePositions.GetPositionByLength(length);

        for (int i = 0; i < length; i ++)
        {
            var position = positions.positions[i];

            item.itemSO.isTheRightOption = i == 2;

            GetComponent<SpriteRenderer>().sprite = item.background;

            var gb = item.itemSO.CreateData();

            gb.transform.parent = transform;

            gb.transform.localPosition = position;

        }

    }

    private void OnDisable()
    {
        dataBridge.action -= Vvv;
    }

    private void OnDestroy()
    {
        
    }

    void Vvv(RightWrongOptionAttachment rw)
    {
        Debug.Log("fasdfasd");
        Debug.Log("选择了" + rw.isTheRightOption);
    }



}

public struct ChooseImgByAudioTemplateStruct
{
    [PreviewField(100), PropertyOrder(-1)]
    public Sprite background;

    [PropertyOrder(1)]
    public CP_ChooseImgByAudioItem_SO itemSO;

    [LabelText("排列位置")]
    public InLinePositions_SO inLinePositions;
}



public enum ChooseImgByAudioTemplate
{
    Shell, Farm
}