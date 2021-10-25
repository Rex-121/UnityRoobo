using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class CoursewarePlayer : SerializedMonoBehaviour
{

    /// <summary>
    /// 课件流程结束
    /// </summary>
    public Action<CoursewarePlayer> DidEndThisCourseware;

    /// <summary>
    /// 课件准备就绪
    /// </summary>
    /// <param name="parent">父节点</param>
    public virtual void CoursewareDidPrepared(Transform parent) { }

   
    public void ClearCoursewarePlayer()
    {
        DidEndThisCourseware = null;
    }

}

