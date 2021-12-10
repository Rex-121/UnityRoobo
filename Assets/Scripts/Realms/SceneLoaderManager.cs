using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{

    public Navigation.Scene needLoadScene;


    private void Start()
    {
        progress.Select(v => v.ToString("0")).Distinct().Subscribe(value =>
        {
            Logging.Log(needLoadScene.ToString() + " 加载 " + value);
        }).AddTo(this);

        progress.Where(v => v >= 1).Take(1).Subscribe(value =>
        {
            Logging.Log(needLoadScene.ToString() + "加载完成");
        }).AddTo(this);
        Observable.EveryEndOfFrame().Take(1).SelectMany(Observable.FromCoroutine(LoadSceneAsync)).Subscribe().AddTo(this);

        RealmNavigation.instance.enterScene += LoadCWScene;
    }

    public void LoadCWScene()
    {
        async.allowSceneActivation = true;
    }

    AsyncOperation async;

    IEnumerator LoadSceneAsync()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        async = SceneManager.LoadSceneAsync(needLoadScene.ToString(), LoadSceneMode.Single);

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

    ReactiveProperty<float> progress = new ReactiveProperty<float>();
}
