using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "CoursewarePlayItem_SO", menuName = "课件SO/课件Player")]
public class CoursewarePlayItem_SO : ScriptableObject
{

    [LabelText("课件")]
    public GameObject coursewarePlayer;

}
