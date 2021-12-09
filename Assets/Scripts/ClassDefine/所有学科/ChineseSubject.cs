using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "人文", menuName = "学科/人文")]
public class ChineseSubject : LanguageSubject
{
    public override ClassSubjectType type => ClassSubjectType.Chinese;

}