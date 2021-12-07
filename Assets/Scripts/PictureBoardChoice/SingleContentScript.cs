using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 单个选项
/// </summary>
public class SingleContentScript : MonoBehaviour
{
    [LabelText("图片内容")]
    public Image mImageContent;

    [LabelText("图片边框")]
    public Image mImageBorder;

    [LabelText("结果反馈图")]
    public Image mImageResult;


}
