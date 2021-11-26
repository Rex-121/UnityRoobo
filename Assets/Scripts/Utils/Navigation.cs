using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "导航", menuName = "单例SO/导航")]
public class Navigation : SingletonSO<Navigation>
{

    //public ClassCategory category;


    //public ClassSubject subject;

    [HideInInspector]
    public ReactiveProperty<Menu> menu = new ReactiveProperty<Menu>(Menu.index);

    [HideInInspector]
    public ReactiveProperty<ClassSubject.Type?> classType;// = new ReactiveProperty<ClassSubject.Type>();

    private ClassSubject _currentType;

    [ShowInInspector]
    public ClassSubject currentType
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

    /// <summary>
    /// 选择了新的学科
    /// </summary>
    /// <param name="type">学科</param>
    public void SetNewClassType(ClassSubject.Type? type)
    {

        string fileName = "";
        switch (type)
        {
            case ClassSubject.Type.Art:
                fileName = "美术";
                break;
            case ClassSubject.Type.Language:
                fileName = "英语";
                break;
        }

        currentType = Resources.Load<ClassSubject>("Class/" + fileName);

        //currentType = (ClassSubject.Type)type;
        //classType.Value = type;
    }

    public void Start()
    {
        classType = new ReactiveProperty<ClassSubject.Type?>();
        currentType = null;
    }


}
