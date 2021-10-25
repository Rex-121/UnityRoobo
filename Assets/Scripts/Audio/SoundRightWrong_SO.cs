using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "默认正确错误音效", menuName = "音效/默认正确错误音效")]
public class SoundRightWrong_SO : ScriptableObject
{

    [LabelText("正确音效")]
    public AudioClip right;


    [LabelText("错误音效")]
    public AudioClip wrong;

}
