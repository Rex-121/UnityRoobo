using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(CoursewareCredit))]
public class CoursewareManager : MonoBehaviour
{

    [LabelText("课件列表")]
    public CoursewarePlaylist playlist;




    private void Start()
    {



        FPS.Instance.LockFrame();



        Next();

    }


    void Next()
    {

        ClearStage();

        var cp = playlist.Next();

        if (cp == null) return;



        gb = Instantiate(cp.coursewarePlayer);

        cp.MakeData(gb, "");

        gb.transform.parent = transform;


        gb.GetComponent<CoursewarePlayer>().DidEndThisCourseware += DidEndACourseware;

        gb.GetComponent<CoursewarePlayer>().creditDelegate = GetComponent<CoursewareCredit>();
    }

    [ReadOnly]
    public GameObject gb;

    public void ClearStage()
    {

        if (gb == null) return;

        var cp = gb.GetComponent<CoursewarePlayer>();

        if (cp != null) cp.DidEndThisCourseware -= DidEndACourseware;

        Destroy(gb);
    }

    void DidEndACourseware(CoursewarePlayer player)
    {
        Debug.Log(player + " End");
        Next();
    }




}