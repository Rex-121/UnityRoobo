using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Diagnostics;

using UniRx;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

using Spine;
using DG.Tweening;
public class RealmsEntrance : MonoBehaviour
{

    ReactiveProperty<float> progress = new ReactiveProperty<float>();

    //public GameObject lessonDetailPre;

    public Transform realmUI;


    public Transform sencUI;


    public Transform imagss;

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

        Logging.Log(UnityEngine.Application.persistentDataPath);


        Stopwatch sw = new Stopwatch();

        sw.Start();

        Logging.Log("file://" + Application.persistentDataPath + "/value.wav");

        WebReqeust.GetAudio("file://" + Application.persistentDataPath + "/value.wav", (c) =>
        {

            Logging.Log("sff" + sw.ElapsedMilliseconds);

            Logging.Log("fasdgadsf");

            GetComponent<AudioSource>().clip = c;

            GetComponent<AudioSource>().Play();

        }, (e) =>
        {

            Logging.Log("error");
            Logging.Log(e);
        });

        //Stopwatch sw = new Stopwatch();

        //sw.Start();

        //WebReqeust.GetAudio("https://dwn.roobo.com/apps/zhixueyuan/dev/pudding/pudding/4.2.2/value.wav", (c) =>
        //{

        //    Logging.Log("sff" + sw.ElapsedMilliseconds);

        //    Logging.Log("fasdgadsf");

        //    GetComponent<AudioSource>().clip = c;

        //    GetComponent<AudioSource>().Play();

        //}, (e) => {

        //    Logging.Log("error");
        //    Logging.Log(e);
        //});
        //var mat = imagss.GetComponent<Image>().materialForRendering;
        //Logging.Log("vafdasdfa" + mat.GetFloat("_ShineLocation"));
        //DOTween.To(() => 0f, (v) =>
        //{
        //    mat.SetFloat("_ShineLocation", Mathf.Min(1, v));
        //}, 1, 1.5f).SetLoops(-1);

    }


    public void ReLoadRealm()
    {
        Logging.Log("fdasfas");
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
