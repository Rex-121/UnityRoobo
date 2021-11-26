using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "二级入口Prefabs", menuName = "学科/二级入口Prefabs")]
public class RealmSecondary_SO : SerializedScriptableObject
{

    [LabelText("美术")]
    public GameObject art;




    public GameObject GetSecondary(ClassSubject.Type type)
    {

        switch (type)
        {
            case ClassSubject.Type.Art:
                return art;
        }
        return new GameObject("Empty");
    }

}
