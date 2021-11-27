using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "二级入口Prefabs", menuName = "学科/二级入口Prefabs")]
public class RealmSecondary_SO : SerializedScriptableObject
{

    [LabelText("美术")]
    public GameObject art;




    public GameObject GetSecondary(学科.类型 type)
    {

        switch (type)
        {
            case 学科.类型.美术:
                return art;
        }
        return new GameObject("Empty");
    }

}
