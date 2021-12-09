using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System;
using Newtonsoft.Json;

public class RealmLevelsList : SerializedMonoBehaviour
{

    //[ShowInInspector]
    //public CourseLevels levles;

    [ShowInInspector]
    public Dictionary<int, CourseLevels_Net.Levels> allLevels
    {
        get
        {
            return levelsDisplay.Value;
        }
    }

    //public CourseLevels_Net.Levels currentLevelDisplay;

    ReactiveProperty<Dictionary<int, CourseLevels_Net.Levels>> levelsDisplay = new ReactiveProperty<Dictionary<int, CourseLevels_Net.Levels>>();


    public Transform listHolder;

    [LabelText("课程Prefab")]
    public GameObject itemPrefab;

    [LabelText("课程等级")]
    public ClassLevels levelsSeek;

    //public class CourseLevels
    //{

    //    public Dictionary<int, CourseLevels_Net.Levels> levels = new Dictionary<int, CourseLevels_Net.Levels>();


    //    public CourseLevels(CourseLevels_Net net)
    //    {
    //        if (net.items != null)
    //        {
    //            for (int i = 0; i < Math.Max(6, net.items.Count); i++)
    //            {
    //                var n = net.items[i];

    //                if (n != null)
    //                {
    //                    levels.Add(i + 1, n);
    //                }
    //            }
    //        }

    //    }
    //}

    IDisposable dis;
    private void Start()
    {
        Observable.CombineLatest(levelsDisplay.Where(v => v != null), levelsSeek.level, (dic, level) =>
        {
            if (dic.ContainsKey(level.value))
            {
                return dic[level.value].lessonList;
            }
            return new List<CourseLevels_Net.Lesson>();
        }).Subscribe(v => XX(v), e => { Logging.Log(e); }).AddTo(this);
    }

    private void OnEnable()
    {

        var a = API.GetCourseLevelsByClassCategory(Navigation.Shared.classCategory);

        dis = a.Subscribe(v =>
         {
             Dictionary<int, CourseLevels_Net.Levels> dic = new Dictionary<int, CourseLevels_Net.Levels>();

             for (int i = 0; i < v.items.Count; i++)
             {
                 dic.Add(i + 1, v.items[i]);
             }

             levelsDisplay.Value = dic;

         }).AddTo(this);


    }

    private void OnDisable()
    {
        dis?.Dispose();

        levelsDisplay.Value = null;

        itemList.ForEach(v => Destroy(v));

        itemList.Clear();

    }

    List<GameObject> itemList = new List<GameObject>();

    void XX(List<CourseLevels_Net.Lesson> lessonList)
    {


        itemList.ForEach(v => Destroy(v));

        itemList.Clear();



        //if (currentLevelDisplay == null || currentLevelDisplay.lessons == null) return;
        for (int i = 0; i < lessonList.Count; i++)
        {

            var item = Instantiate(itemPrefab, listHolder);


            var itemSc = item.GetComponent<RealmLevelListItem>();

            var itemValue = new RealmLevelListItem.IndexedItem(i + 1, Navigation.Shared.currentSubject.theme, lessonList[i]);

            itemSc.SetItem(itemValue);

            itemList.Add(item);
        }

    }

}
