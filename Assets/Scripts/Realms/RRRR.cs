using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Newtonsoft.Json.Linq;

public class RRRR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Logging.Log("RRRR");

        API.GetCoursePlayInfo().Subscribe(v =>
        {
            Logging.Log(v);


            //Logging.Log(v.queue);
            //Forge

        }, e =>
        {

            Logging.Log(e);

            Logging.Log((e as HttpError).message);
        });

       

        HttpRx.Get<ForgeData.RoundList>("/pudding/teacher/v1/course/5509/lesson/6979/round/list").Select(UpdateRoundValue).Subscribe(v =>
        {
            Logging.Log(v);
        }, e =>
        {

            Logging.Log(e);

            Logging.Log((e as HttpError).message);
        });
    }

    RoundQueue UpdateRoundValue(ForgeData.RoundList rList)
    {
        List<Round> r = new List<Round>(rList.list.Count);

        foreach (var v in rList.list)
        {
            r.Add(new Round(v));
        }

        var rQueue = new RoundQueue(r);

        Logging.Log(rQueue.FlowDescription());

        return rQueue;
    }
}
