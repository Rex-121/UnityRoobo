using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class CoursewarePlayer : SerializedMonoBehaviour, CoursewarePlayOrder
{

    [Header("事件代理")]

    /// <summary>
    /// 课件流程结束
    /// </summary>
    public Action<CoursewarePlayer> DidEndThisCourseware;


    /// <summary>
    /// 分数事件代理
    /// </summary>
    public CoursewareCreditProtocol creditDelegate;



    /// <summary>
    /// 课件准备就绪
    /// </summary>
    /// <param name="parent">父节点</param>
    public virtual void CoursewareDidPrepared(Transform parent) { }

   
    public void ClearCoursewarePlayer()
    {
        DidEndThisCourseware = null;
    }

    public virtual void DidStartCourseware()
    {
        
    }

    public virtual void DidEndCourseware()
    {
       
    }
}



public interface CoursewarePlayOrder
{

    public void DidStartCourseware();


    public void DidEndCourseware();

}

