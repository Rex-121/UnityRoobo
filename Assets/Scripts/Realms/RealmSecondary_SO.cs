using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "二级入口Prefabs", menuName = "学科/二级入口Prefabs")]
public class RealmSecondary_SO : SerializedScriptableObject
{

    public Dictionary<ClassSubjectType, GameObject> entrances = new Dictionary<ClassSubjectType, GameObject>();

    public GameObject GetSecondary(ClassSubjectType type)
    {

        if (entrances.ContainsKey(type))
        {
            return entrances[type];
        }

        return new GameObject("Empty");
    }

}
