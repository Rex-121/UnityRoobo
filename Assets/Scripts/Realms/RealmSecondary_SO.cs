using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "二级入口Prefabs", menuName = "学科/二级入口Prefabs")]
public class RealmSecondary_SO : SerializedScriptableObject
{

    public Dictionary<学科.类型, GameObject> entrances = new Dictionary<学科.类型, GameObject>();

    public GameObject GetSecondary(学科.类型 type)
    {

        if (entrances.ContainsKey(type))
        {
            return entrances[type];
        }

        return new GameObject("Empty");
    }

}
