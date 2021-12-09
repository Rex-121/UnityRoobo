using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UniRx;
using Newtonsoft.Json;

public class RealmThird : MonoBehaviour
{

    [LabelText("课程Prefab")]
    public GameObject itemPrefab;


    public Transform list;


    [LabelText("课程等级")]
    public ClassLevels levels;


    public Image listBg;


    public RealmLevelsList listing;
    public RealmThird_Item_SO theme;

    private void OnEnable()
    {
        listBg.sprite = theme.itemTheme[Navigation.Shared.classCategory.subject.type].backgroundBG;

    }

}
