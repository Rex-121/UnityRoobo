using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RWItemSprite_SO", menuName = "对错Item/图片模版/对错")]
public class RWItemSprite_SO : ScriptableObject
{

    public string template = "模版";

    public Sprite prepare;

    public Sprite idle;

    public Sprite normal;

    public Sprite right;

    public Sprite wrong;

}
