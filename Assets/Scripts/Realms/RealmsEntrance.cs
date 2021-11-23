using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Diagnostics;

using UniRx;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class RealmsEntrance : MonoBehaviour
{

    ReactiveProperty<float> progress = new ReactiveProperty<float>();

    //public GameObject lessonDetailPre;

    private void OnEnable()
    {
        FPS.Default.LockFrame();

        progress.Distinct().Subscribe(value =>
        {
            Logging.Log("SampleScene 加载 " + value);
        }).AddTo(this);

        progress.Where(v => v >= 1).Take(1).Subscribe(value =>
        {
            Logging.Log("SampleScene 加载完成");
        }).AddTo(this);

        Observable.EveryEndOfFrame().Take(1).SelectMany(Observable.FromCoroutine(LoadSceneAsync)).Subscribe().AddTo(this);

        Logging.Log(SystemInfo.graphicsDeviceName);

        Logging.Log(SystemInfo.graphicsDeviceVendor);

        Logging.Log(Application.persistentDataPath);


    }


    public void LoadCWScene()
    {
        async.allowSceneActivation = true;
    }

    private void OnMouseDown()
    {
        //async.allowSceneActivation = true;



        Stopwatch sw = new Stopwatch();

        var gb = Resources.Load("LessonDetails", typeof(GameObject));

        Logging.Log("加载afd " + sw.ElapsedMilliseconds);

        Instantiate(gb);

        gameObject.SetActive(false);

        sw.Stop();

        Logging.Log("生成afd " + sw.ElapsedMilliseconds);
    }

    AsyncOperation async;

    IEnumerator LoadSceneAsync()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        async = SceneManager.LoadSceneAsync("Courseware");


        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            progress.Value = async.progress;
            yield return new WaitForEndOfFrame();
        }

        progress.Value = 1;

        sw.Stop();

        Logging.Log("加载完成 " + sw.ElapsedMilliseconds + "ms");
    }
}
