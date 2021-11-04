using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "连线SO", menuName = "课件/连线/连线SO")]
public class PictureMatchItem_SO : ScriptableObject
{
    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite normal;


    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite right;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite wrong;

    [PreviewField(50, ObjectFieldAlignment.Left)]
    public Sprite choosen;

}
