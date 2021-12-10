using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;
using System.Linq;

public class RealmForthList : SerializedMonoBehaviour
{
    public RealmThird_Item_SO theme;

    public RealmForthDataConnect_SO dataSO;

    [ShowInInspector]
    public RoundQueue queue;

    public Image bookImage;

    public GameObject itemPrefab;

    public List<Transform> items;

    private void Start()
    {

        queue = dataSO.queue;
        bookImage.sprite = theme.itemTheme[Navigation.Shared.classCategory.subject.type].bookBackGround;

        var list = queue.round.allDisplayInRealm;


        for (int i = 0; i < Mathf.Min(list.Count, 4); i++)
        {

            var item = Instantiate(itemPrefab, items[i]);
            item.GetComponent<RealmForthListItem>().SetRound(list[i]);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
