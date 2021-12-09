using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "四级页面数据连接", menuName = "课程/四级页面数据连接")]
public class RealmForthDataConnect_SO : ScriptableObject
{
    [HideInInspector]
    public RoundQueue queue;

}
