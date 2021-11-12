using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using System.Diagnostics;
using System.IO;

public class RealmCanvas : MonoBehaviour
{


    [SerializeField]
    private GameObject island;

    [SerializeField]
    private GameObject releam;

    System.IDisposable a;

    private void Start()
    {
        releam.SetActive(true);
        island.SetActive(false);

        a = Observable.EveryEndOfFrame().Take(1).SelectMany(LoadSceneAsync).Subscribe();

        //var v = Path.Combine(Application.streamingAssetsPath, "adfa.csv");

        //var k = CSV.Instance.ReadFromFile(v, "adfa.csv");
        //Logging.Log(k);
        //k.ForEach(v => Logging.Log(v));
    }

    AsyncOperation async;

    IEnumerator LoadSceneAsync()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        async = SceneManager.LoadSceneAsync("SampleScene");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            Logging.Log("加载进度" + async.progress);
            yield return new WaitForEndOfFrame();
        }

        sw.Stop();

        Logging.Log("加载完成 " + sw.ElapsedMilliseconds + "ms");
        Logging.Log("加载完成" + async.progress);
    }


    IEnumerator LoadResourceAsync()
    {
        Stopwatch sw = new Stopwatch();
        Logging.Log("FPS开始 " + sw.ElapsedMilliseconds + "ms");
        sw.Start();
        FPS.Default.LockFrame();
        //Application.targetFrameRate = 60;

        //var gb = new GameObject("FPSDisplay");

        //gb.AddComponent<FPSDisplayOnGUI>();
        yield return new WaitForEndOfFrame();

        sw.Stop();

        Logging.Log("FPS完成 " + sw.ElapsedMilliseconds + "ms");
    }

    public void MenuToggle()
    {
        releam.SetActive(!releam.activeSelf);
        island.SetActive(!releam.activeSelf);
    }
    private void OnDisable()
    {
        a.Dispose();
    }


    public void LoadScene()
    {

        async.allowSceneActivation = true;
        //SceneManager.LoadScene("SampleScene");

    }

}

