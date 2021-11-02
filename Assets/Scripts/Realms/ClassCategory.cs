using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;





[CreateAssetMenu(fileName = "延时课", menuName = "课程/延时课")]
public class AfterClass : ClassCategory
{


}




public abstract class ClassCategory: ScriptableObject
{


    public ClassSubject subject;


    //public ClassSubject subject;

}


/// <summary>
/// 学科
/// </summary>
public abstract class ClassSubject : ScriptableObject
{

}
