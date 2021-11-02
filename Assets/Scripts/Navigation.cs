using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "导航", menuName = "单例SO/导航")]
public class Navigation : SingletonSO<Navigation>
{

    public ClassCategory category;


    public ClassSubject subject;

    private void OnEnable()
    {
        category = null;

        subject = null;
    }
   

    public void SetCurrentClassCategory(ClassCategory obj)
    {
        category = obj;
    }

    public void SetCurrentClassSubject(ClassSubject subject)
    {
        this.subject = subject;
        category.subject = this.subject;
    }

}
