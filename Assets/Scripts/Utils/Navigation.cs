using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(fileName = "导航", menuName = "单例SO/导航")]
public class Navigation : SingletonSO<Navigation>
{

    public enum Scene
    {
        Realm, Courseware
    }

    public void CurrentScene(Scene value)
    {
        scene.Value = value;
    }

    [HideInInspector]
    ReactiveProperty<Scene> scene = new ReactiveProperty<Scene>(Scene.Realm);

    public IObservable<Scene> sceneRx
    {
        get { return scene; }
    }

    [HideInInspector]
    public ReactiveProperty<Menu> menu = new ReactiveProperty<Menu>(Menu.index);

    [HideInInspector]
    public ReactiveProperty<ClassSubjectType?> classType = new ReactiveProperty<ClassSubjectType?>();

    private ClassSubject _currentType;

    [LabelText("课程"), ShowInInspector]
    public ClassCategory classCategory
    {
        set
        {
            _classCategory = value;
        }

        get { return _classCategory; }
    }
    private ClassCategory _classCategory;

    [ShowInInspector]
    public ClassSubject currentSubject
    {
        set
        {
            _currentType = value;
            if (_currentType == null)
            {
                menu.Value = Menu.index;
                classType.Value = null;
            }
            else
            {

                classType.Value = _currentType.type;
            }
        }

        get { return _currentType; }
    }

    [System.Serializable]
    public enum Menu
    {
        index, secondary, third, forth
    }


    public void 选择菜单(Menu menu)
    {
        this.menu.Value = menu;
    }

    public void 选择延时课()
    {
        classCategory = Resources.Load<ClassCategory>("Class/延时课");
        classCategory.subject = currentSubject;
    }

    /// <summary>
    /// 选择了新的学科
    /// </summary>
    /// <param name="type">学科</param>
    public void 切换学科(ClassSubjectType? sType)
    {
        currentSubject = Resources.Load<ClassSubject>("Class/" + sType.SO文件名());
    }


    public void Start()
    {
        classCategory = null;
        currentSubject = null;
    }


}
