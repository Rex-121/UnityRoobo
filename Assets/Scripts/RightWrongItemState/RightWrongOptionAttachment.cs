using UnityEngine;
using Sirenix.OdinInspector;

[AddComponentMenu("对错/对错选项")]
public class RightWrongOptionAttachment : MonoBehaviour
{
    [LabelText("是否是正确选项"), PropertySpace(SpaceBefore = 20f, SpaceAfter = 20f)]
    public bool isTheRightOption = false;

}
