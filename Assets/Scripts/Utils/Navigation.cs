using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "导航", menuName = "单例SO/导航")]
public class Navigation : SingletonSO<Navigation>
{

    [HideInInspector]
    public ReactiveProperty<Menu> menu = new ReactiveProperty<Menu>(Menu.index);

    [HideInInspector]
    public ReactiveProperty<ClassSubjectType?> classType = new ReactiveProperty<ClassSubjectType?>();

    private ClassSubject _currentType;

    [LabelText("课程")]
    public ClassCategory classCategory
    {
        set
        {
            _classCategory = value;
            if (_classCategory == null)
            {
                menu.Value = Menu.secondary;
            }
            else
            {
                menu.Value = Menu.third;
            }
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
                menu.Value = Menu.secondary;
                classType.Value = _currentType.type;
            }
        }

        get { return _currentType; }
    }

    [System.Serializable]
    public enum Menu
    {
        index, secondary, third
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
        //classType = new ReactiveProperty<ClassSubject.Type?>();
        currentSubject = null;
    }


}
