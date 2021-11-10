using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Diagnostics;


public class CoursewareLoader : MonoBehaviour
{

    //[SerializeField]
    //GameObject coursewarePre;

    // Start is called before the first frame update
    void Start()
    {

        Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var a = Resources.Load("CoursewareManager");

            Instantiate(a);

            sw.Stop();

            UnityEngine.Debug.Log(string.Format("total: {0} ms", sw.ElapsedMilliseconds));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
