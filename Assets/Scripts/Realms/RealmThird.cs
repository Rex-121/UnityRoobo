using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    public RealmThird_Item_SO theme;

    // Start is called before the first frame update
    void Start()
    {

        
        levels.level.Subscribe(lev =>
        {

            Logging.Log("现在等级" + lev.value);
        });

        Navigation.Shared.classType.Where(v => v != null).Subscribe(v =>
         {
             for (int i = 0; i < 12; i++)
             {

                 var item = Instantiate(itemPrefab, list);


                 var itemSc = item.GetComponent<RealmLevelListItem>();
                 var stir = @"{'icon': 'afsa', 'name': 'vvv', 'locked': true}";
                 var itemValue = new RealmLevelListItem.IndexedItem(i, Navigation.Shared.currentSubject.theme, JsonConvert.DeserializeObject<RealmLevelListItem.Item>(stir));

                 itemSc.SetItem(itemValue);

             }
         });

    }

}
