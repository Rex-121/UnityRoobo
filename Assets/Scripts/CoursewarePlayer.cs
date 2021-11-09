using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class CoursewarePlayer : SerializedMonoBehaviour, CoursewareLifetime
{

    [Header("事件代理")]


    /// <summary>
    /// 分数事件代理
    /// </summary>
    public CoursewareCreditProtocol creditDelegate;



    /// <summary>
    /// 课件准备就绪
    /// </summary>
    /// <param name="parent">父节点</param>
    public virtual void CoursewareDidPrepared(Transform parent) { }



    public CoursewareLifetime lifetimeDelegate;


    public void ClearCoursewarePlayer()
    {
        creditDelegate = null;
    }

    public virtual void DidLoadCourseware(CoursewarePlayer player)
    {
        lifetimeDelegate?.DidLoadCourseware(player);
    }

    public virtual void DidReadyToPlayCourseware(CoursewarePlayer player)
    {
        lifetimeDelegate?.DidReadyToPlayCourseware(player);
    }

    public virtual void DidStartCourseware(CoursewarePlayer player)
    {
        lifetimeDelegate?.DidStartCourseware(player);
    }

    public virtual void DidEndCourseware(CoursewarePlayer player)
    {
        lifetimeDelegate?.DidEndCourseware(player);
    }

}



public interface CoursewareLifetime
{
    /// <summary>
    /// 加载完成
    /// </summary>
    public void DidLoadCourseware(CoursewarePlayer player);

    // 可以游玩
    public void DidReadyToPlayCourseware(CoursewarePlayer player);

    // 开始游玩
    public void DidStartCourseware(CoursewarePlayer player);

    // 结束游玩
    public void DidEndCourseware(CoursewarePlayer player);

}


public class CoursewareLifetimeListener : CoursewareLifetime
{


    public Action<CoursewarePlayer> load, ready, start, end;


    public CoursewareLifetimeListener(Action<CoursewarePlayer> load, Action<CoursewarePlayer> ready, Action<CoursewarePlayer> start, Action<CoursewarePlayer> end)
    {
        this.load = load;
        this.ready = ready;
        this.start = start;
        this.end = end;
    }


    public void DidEndCourseware(CoursewarePlayer player)
    {
        end(player);
    }

    public void DidLoadCourseware(CoursewarePlayer player)
    {
        load(player);
    }

    public void DidReadyToPlayCourseware(CoursewarePlayer player)
    {
        ready(player);
    }

    public void DidStartCourseware(CoursewarePlayer player)
    {
        start(player);
    }
}