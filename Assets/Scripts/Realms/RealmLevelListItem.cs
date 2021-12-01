using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RealmLevelListItem : MonoBehaviour
{

    public struct Item
    {
        public string icon;

        public string name;

        public bool locked;
    }

    
    public struct IndexedItem
    {
        public Item item;

        public int index;

        public Color theme;

        public IndexedItem(int i, Color t, Item it)
        {
            index = i;
            item = it;
            theme = t;
        }
    }

    [ShowInInspector]
    public IndexedItem item;

    public void SetItem(IndexedItem i)
    {
        item = i;

        badge.color = i.theme;

        SetBtnEnable(item.item.locked);

        indexLabel.text = item.index.ToString();

        nameLabel.text = item.item.name;
    }


    //public Color themeColor;


    public Image badge;


    public Text indexLabel;

    public Text nameLabel;

    public Image lockImage;
    public Button selfBtn;

    void SetBtnEnable(bool enable)
    {
        selfBtn.interactable = !enable;
        lockImage.gameObject.SetActive(enable);
    }
}
