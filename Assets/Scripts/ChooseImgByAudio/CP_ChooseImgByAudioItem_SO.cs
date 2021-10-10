using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Bolt;

[CreateAssetMenu(fileName = "音选图item", menuName = "课件/音选图/音选图item")]
public class CP_ChooseImgByAudioItem_SO : ScriptableObject
{

    [Required]
    public GameObject itemPrefab;

    [Required]
    [LabelText("RW状态化的item")]
    public RWItemSprite_SO rwItemSO;

    [LabelText("是否是正确选项")]
    public bool isTheRightOption = false;


    public GameObject CreateData()
    {

        GameObject gb = Instantiate(itemPrefab);

        SetSprite(gb);

        SetRightOptionIfNeeded(gb);

        return gb;

    }


    private void SetSprite(GameObject gb) {

        var flowVariables = gb.GetComponent<Variables>();

        if (flowVariables == null) return;

        flowVariables.declarations.Set("RWItemSpite_SO", rwItemSO);

    }



    // 设置正确错误
    private void SetRightOptionIfNeeded(GameObject gb)
    {
        var rwAttachment = gb.GetComponent<RightWrongOptionAttachment>();

        if (rwAttachment == null) return;

        rwAttachment.isTheRightOption = isTheRightOption;
    }
}
