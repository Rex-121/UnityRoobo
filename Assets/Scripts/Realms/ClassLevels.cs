using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLevels : MonoBehaviour
{

    public GameObject prefab;

    void Start()
    {
        for (int i = 0; i < 6; i ++)
        {
           var gb = Instantiate(prefab, transform);
            gb.GetComponent<ClassLevelsItem>().index = i;
        }
    }

}
