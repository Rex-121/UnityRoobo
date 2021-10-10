using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "RWItemSprite_SO", menuName = "对错Item/图片模版/对错")]
public class RWItemSprite_SO : ScriptableObject
{

    public string template = "模版";

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite prepare;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite idle;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite normal;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite right;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite wrong;

}
