using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Diagnostics;

using UniRx;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class RealmsEntrance : MonoBehaviour
{

    ReactiveProperty<float> progress = new ReactiveProperty<float>();

    [LabelText("一级页面")]
    public Transform index;

    [LabelText("二级页面")]
    public Transform sencondry;

    [LabelText("三级页面")]
    public Transform third;


    private void Start()
    {
        FPS.Default.LockFrame();

        progress.Select(v=>v.ToString("0")).Distinct().Subscribe(value =>
        {
            Logging.Log("SampleScene 加载 " + value);
        }).AddTo(this);

        progress.Where(v => v >= 1).Take(1).Subscribe(value =>
        {
            Logging.Log("SampleScene 加载完成");
        }).AddTo(this);

        Observable.EveryEndOfFrame().Take(1).SelectMany(Observable.FromCoroutine(LoadSceneAsync)).Subscribe().AddTo(this);

        Navigation.Shared.menu.Subscribe(菜单 =>
        {
            index.gameObject.SetActive(菜单 == Navigation.菜单.一级);
            sencondry.gameObject.SetActive(菜单 == Navigation.菜单.二级);
            third.gameObject.SetActive(菜单 == Navigation.菜单.三级);
        }).AddTo(this);

    }


    public void ReLoadRealm()
    {
        SceneManager.LoadScene("Realm");
    }

    /// <summary>
    /// 打开二级菜单
    /// </summary>
    /// <param name="value"></param>
    public void DidNeedPushSecondaryMenu(string value)
    {
        学科.类型 classType = 学科.类型.美术;

        switch (value)
        {
            case "美术":
                classType = 学科.类型.美术;
                break;
            case "语言":
                classType = 学科.类型.语言;
                break;
            case "音乐":
                classType = 学科.类型.音乐;
                break;
        }

        Navigation.Shared.切换学科(classType);
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
        async = SceneManager.LoadSceneAsync("Courseware", LoadSceneMode.Single);

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



    /// <summary>
    /// 打开设置页面
    /// </summary>
    public void PushSettingPage()
    {
        NativeCalls.Shared.PushSettingMenus();
        //if (User.Shared.isLogin)
        //{
        //    NativeCalls.Shared.PushSettingMenus();
        //}
        //else
        //{
        //    SceneManager.LoadScene("LoginScence");
        //}

    }
}
