using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

using UniRx;
using System;
public class RealmLevelListItem : MonoBehaviour
{

    //public struct Item
    //{
    //    public string icon;

    //    public string name;

    //    public bool locked;
    //}


    public struct IndexedItem
    {
        public CourseLevels_Net.Lesson item;

        public int index;

        public Color theme;

        public IndexedItem(int i, Color t, CourseLevels_Net.Lesson it)
        {
            index = i;
            item = it;
            theme = t;
        }
    }

    [ShowInInspector]
    public IndexedItem item;



    public void FourthMenu()
    {

    }


    public void SetItem(IndexedItem i)
    {
        item = i;

        badge.color = i.theme;

        SetBtnEnable(item.item.locked);

        indexLabel.text = item.index.ToString();

        nameLabel.text = item.item.name;

        if (!item.item.locked)
        {


      Storage.GetImage(new Parcel(item.item.icon)).Subscribe(v =>
            {
               
                try
                {
                    iconImage.sprite = v;
                }
                catch (Exception e)
                {
                    Logging.Log(e.Message);
                }
            }).AddTo(this);
           


        }

    }



    //public Color themeColor;


    public Image badge;


    public Image iconImage;

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
