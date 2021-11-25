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

    public Transform realmUI;


    public Transform sencUI;


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

        //Navigation.Shared


        WebReqeust.GetAudio("https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-07-13/c3ml5t0rjdcmt7uaegqg.mp3", (c) =>
        {
            Logging.Log("fasgas");
            GetComponent<AudioSource>().clip = c;
            GetComponent<AudioSource>().Play();
        }, (e) =>
        {
            Logging.Log(e);
        });

    }


    public void ReLoadRealm()
    {
        SceneManager.LoadScene("Realm");
    }


    public Image[] imgs;

    string indexing;

    public GameObject ship;

    public void ToggleSecondary(string index)
    {

        realmUI.gameObject.SetActive(false);

        ship.SetActive(false);

        foreach (var i in imgs)
        {
            i.gameObject.SetActive(false);
        }

        switch (index)
        {
            case "0":
                imgs[0].gameObject.SetActive(true);
                break;
            case "1":
                imgs[1].gameObject.SetActive(true);
                break;
            case "2":
                imgs[2].gameObject.SetActive(true);
                break;
            case "3":
                imgs[3].gameObject.SetActive(true);
                break;
            case "4":
                imgs[4].gameObject.SetActive(true);
                break;
        }

        if (index == indexing)
        {
            realmUI.gameObject.SetActive(true);
            indexing = null;
            ship.SetActive(true);
        }
        else
        {
            indexing = index;
        }


        //bool display = sencUI.gameObject.activeInHierarchy;

        //realmUI.gameObject.SetActive(display);
        //sencUI.gameObject.SetActive(!display);
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
