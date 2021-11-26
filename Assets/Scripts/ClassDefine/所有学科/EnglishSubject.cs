using UnityEngine;

[CreateAssetMenu(fileName = "英语", menuName = "学科/英语")]
public class EnglishSubject : ClassSubject
{
    public override Type type => Type.Language;

    public override string fileName => "英语";
}
