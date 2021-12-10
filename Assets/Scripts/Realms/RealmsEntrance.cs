using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Diagnostics;

using UniRx;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class RealmsEntrance : MonoBehaviour
{

    [LabelText("一级页面")]
    public Transform index;

    [LabelText("二级页面")]
    public Transform sencondry;

    [LabelText("三级页面")]
    public Transform third;

    [LabelText("四级页面")]
    public Transform forth;

    private void Start()
    {

        Navigation.Shared.menu.Subscribe(menu =>
        {
            index.gameObject.SetActive(menu == Navigation.Menu.index);
            sencondry.gameObject.SetActive(menu == Navigation.Menu.secondary);
            third.gameObject.SetActive(menu == Navigation.Menu.third);
            forth.gameObject.SetActive(menu == Navigation.Menu.forth);
        }).AddTo(this);

        Navigation.Shared.CurrentScene(Navigation.Scene.Realm);

    }

    /// <summary>
    /// 打开二级菜单
    /// </summary>
    /// <param name="value"></param>
    public void DidNeedPushSecondaryMenu(string value)
    {
        ClassSubjectType classType = ClassSubjectType.Art;

        switch (value)
        {
            case "美术":
                classType = ClassSubjectType.Art;
                break;
            case "语言":
                classType = ClassSubjectType.Language;
                break;
            case "音乐":
                classType = ClassSubjectType.Music;
                break;
        }

        Navigation.Shared.切换学科(classType);

        Navigation.Shared.选择菜单(Navigation.Menu.secondary);
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
