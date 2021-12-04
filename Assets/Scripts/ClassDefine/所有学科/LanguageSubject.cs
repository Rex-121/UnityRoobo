using UnityEngine;

[CreateAssetMenu(fileName = "语言", menuName = "学科/语言")]
public class LanguageSubject : ClassSubject
{
    public override ClassSubjectType type => ClassSubjectType.Language;
}
