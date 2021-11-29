using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "导航", menuName = "单例SO/导航")]
public class Navigation : SingletonSO<Navigation>
{

    [HideInInspector]
    public ReactiveProperty<菜单> menu = new ReactiveProperty<菜单>(菜单.一级);

    [HideInInspector]
    public ReactiveProperty<学科.类型?> classType;// = new ReactiveProperty<ClassSubject.Type>();

    private 学科 _currentType;

    [ShowInInspector]
    public 学科 当前学科
    {
        set
        {
            _currentType = value;
            if (_currentType == null)
            {
                menu.Value = 菜单.一级;
                classType.Value = null;
            }
            else
            {
                menu.Value = 菜单.二级;
                classType.Value = _currentType.type;
            }
        }

        get { return _currentType; }
    }

    [System.Serializable]
    public enum 菜单
    {
        一级, 二级, 三级
    }

    /// <summary>
    /// 选择了新的学科
    /// </summary>
    /// <param name="type">学科</param>
    public void 切换学科(学科.类型? 学科类型)
    {
        当前学科 = Resources.Load<学科>("Class/" + 学科类型.SO文件名());
    }


    public void Start()
    {
        classType = new ReactiveProperty<学科.类型?>();
        当前学科 = null;
    }


}
