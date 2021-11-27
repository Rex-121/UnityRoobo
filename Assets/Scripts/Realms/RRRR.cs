using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RRRR : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Logging.Log("RRRR");

        HttpRx.Get<string>("/pudding/teacher/v1/course/5509/lesson/6965/play/info").Subscribe(v =>
        {
            Logging.Log(v);
        }, e => {

            Logging.Log(e);

            Logging.Log((e as HttpError).message);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
