using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 图片选择题
/// </summary>
public class CW_PictureBoardChoice : CoursewarePlayer
{
    [LabelText("画板载体Grid")]
    public GridLayoutGroup mCarrierGrid;

    [LabelText("选项内容Grid")]
    public GridLayoutGroup mContentGrid;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="entity"></param>
    public void InitGridAndData(CW_PictrueBoardChoice_SO.PictureBoardEntity entity)
    {

    }
}
