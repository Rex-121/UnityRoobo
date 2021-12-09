using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class CoursewareRoundingList : SerializedMonoBehaviour
{
    [HideInInspector]
    public List<RoundIsPlaying> roundList = new List<RoundIsPlaying>();


    [HideInInspector]
    public BehaviorSubject<RoundIsPlaying> round;

    public void SetRoundList(List<RoundIsPlaying> list)
    {
        roundList.AddRange(list);
    }

    public void Next()
    {
        if (rounding.next == null)
        {
            SceneManager.LoadScene("Realm");
            return;
        }

        Logging.Log("请求下一个round ->>" + rounding.next.type);
        round.OnNext(rounding.next);
    }

    private void Awake()
    {
        round = new BehaviorSubject<RoundIsPlaying>(null);
    }

    public void Merge()
    {



        LeadingRound leading = new LeadingRound();


        RoundIsPlaying start = leading;

        foreach (RoundIsPlaying roundIsPlaying in roundList)
        {
            roundIsPlaying.previous = start;
            start.next = roundIsPlaying;

            start = roundIsPlaying;
        }

        

        try
        {
            round.OnNext(leading.RemoveSelf());
        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }

    }




    [ShowInInspector]
    private RoundIsPlaying rounding => round != null ? round.Value : null;
}
