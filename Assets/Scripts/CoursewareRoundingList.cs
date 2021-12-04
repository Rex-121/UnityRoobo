using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;

public class CoursewareRoundingList : SerializedMonoBehaviour
{
    [HideInInspector]
    public List<RoundIsPlaying> roundList = new List<RoundIsPlaying>();

    public void SetRoundList(List<RoundIsPlaying> list)
    {
       
        roundList.AddRange(list);

        pin = Pin.Start();
        pin.max = roundList.Count;
    }

    [HideInInspector]
    public RoundIsPlaying currentRound;


    public RoundIsPlaying GetRound()
    {

        pin.Increase();

        if (pin.end) return null;

        currentRound = roundList[pin.index];

        return currentRound;
    }

    [ShowInInspector, LabelText("RoundIsPlaying.List<CW_OriginContent>")]
    public List<List<CW_OriginContent>> theListxxx
    {
        get
        {
            var f = roundList.Select(v => v.process.Select(vv => vv).ToList()).ToList();
            return f;
        }
    }
    [ShowInInspector, Title("当前Round"), HideLabel, PropertySpace(SpaceAfter = 30), PropertyOrder(-20)]
    public RoundIsPlaying RoundLoaded
    {
        get
        {
            return currentRound;
        }
    }

    [InlineProperty, PropertyOrder(-10)]
    public Pin pin = new Pin(-1, 0);
    //[Button]

    public struct Pin
    {
        [ReadOnly]
        public int index;

        public Pin(int i, int m) { index = i; max = m; }

        public static Pin Start() => new Pin(-1, 0);
        [ReadOnly]
        public int max;

        [ShowInInspector]
        public bool end => index >= max;

        public void Increase()
        {
            index += 1;
        }

    }
}
