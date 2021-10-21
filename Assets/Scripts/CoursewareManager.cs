using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class CoursewareManager : MonoBehaviour
{

    [LabelText("课件列表")]
    public List<CoursewarePlayItem_SO> playlist;


    void Start()
    {

        var gb = playlist[0].coursewarePlayer;

        var item = Instantiate(gb);

        item.transform.parent = transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
