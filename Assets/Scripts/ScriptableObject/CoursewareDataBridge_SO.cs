using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using UnityEngine.Events;


public class CoursewareDataBridge_SO : SerializedScriptableObject
{

    [SerializeField]
    private List<GameObject> allItems = new List<GameObject>();

    //public void DidBeginCourseware()
    //{

    //}

    //public void DidEndCourseware()
    //{

    //}

    public void AddNewItem(GameObject gb)
    {
        allItems.Add(gb);
    }

    public void RemoveAllItem()
    {
        allItems.RemoveAll((_) => { return true; });
    }

}
