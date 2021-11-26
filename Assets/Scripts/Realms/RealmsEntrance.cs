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


    private void Start()
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

        Navigation.Shared.menu.Subscribe(menu =>
        {
            index.gameObject.SetActive(menu == Navigation.Menu.index);
            sencondry.gameObject.SetActive(menu == Navigation.Menu.secondary);
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
        ClassSubject.Type classType = ClassSubject.Type.Art;

        switch (value)
        {
            case "2":
                classType = ClassSubject.Type.Art;
                break;
            case "1":
                classType = ClassSubject.Type.Language;
                break;
        }

        Navigation.Shared.SetNewClassType(classType);
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



    /// <summary>
    /// 打开设置页面
    /// </summary>
    public void PushSettingPage()
    {
        NativeCalls.Default.PushSettingMenus();
    }
}
