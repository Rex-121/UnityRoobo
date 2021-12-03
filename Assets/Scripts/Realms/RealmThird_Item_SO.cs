using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "三级页面Item主题", menuName = "课程/三级页面Item主题")]
public class RealmThird_Item_SO : SerializedScriptableObject
{

    public struct Item
    {
        public Color tintColor;
    }


    public Dictionary<学科.类型, Item> itemTheme = new Dictionary<学科.类型, Item>();

}