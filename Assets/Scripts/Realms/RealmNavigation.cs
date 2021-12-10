using System;
using System.Collections;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealmNavigation : MonoBehaviour
{

    public static RealmNavigation instance = null;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }

      


        //progress.Select(v => v.ToString("0")).Distinct().Subscribe(value =>
        //{
        //    Logging.Log(needLoadScene.ToString() + " 加载 " + value);
        //}).AddTo(this);

        //progress.Where(v => v >= 1).Take(1).Subscribe(value =>
        //{
        //    Logging.Log(needLoadScene.ToString() + "加载完成");
        //}).AddTo(this);





        //Navigation.Shared.sceneRx.DistinctUntilChanged().Subscribe(scene =>
        //{
        //    sceneLoadDis?.Dispose();

        //    FPS.Shared.LockFrame();

        //    async = null;

        //    switch (scene)
        //    {
        //        case Navigation.Scene.Realm:
        //            needLoadScene = Navigation.Scene.Courseware;
        //            break;
        //        case Navigation.Scene.Courseware:
        //            needLoadScene = Navigation.Scene.Realm;
        //            break;
        //    }

        //    sceneLoadDis = Observable.EveryEndOfFrame().Take(1).SelectMany(Observable.FromCoroutine(LoadSceneAsync)).Subscribe().AddTo(this);

        //});

    }

    private void Start()
    {
        Navigation.Shared.Start();
    }

    [ShowInInspector]
    public Action enterScene;




    //Navigation.Scene needLoadScene;

    //IDisposable sceneLoadDis;


    public void LoadCWScene()
    {
        enterScene?.Invoke();
    }

    //AsyncOperation async;

    //IEnumerator LoadSceneAsync()
    //{
    //    Stopwatch sw = new Stopwatch();

    //    sw.Start();

    //    async = SceneManager.LoadSceneAsync(needLoadScene.ToString(), LoadSceneMode.Single);

    //    async.allowSceneActivation = false;

    //    while (async.progress < 0.9f)
    //    {
    //        progress.Value = async.progress;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    progress.Value = 1;

    //    sw.Stop();

    //    Logging.Log("加载完成 " + sw.ElapsedMilliseconds + "ms");
    //}

    //ReactiveProperty<float> progress = new ReactiveProperty<float>();

}
