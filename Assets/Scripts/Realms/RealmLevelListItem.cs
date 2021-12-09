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


    public RealmForthDataConnect_SO dataSO;

    public void FourthMenu()
    {
        dataSO.queue = item.item.queue;
        Navigation.Shared.menu.Value = Navigation.Menu.forth;
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
                      if (iconImage == null) return;
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

        rounding = item.item.Merge();

    }


    [ShowInInspector]
    public RoundIsPlaying rounding;


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
