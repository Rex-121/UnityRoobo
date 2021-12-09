using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "英语", menuName = "学科/英语")]
public class EnglishSubject : LanguageSubject
{
    public override ClassSubjectType type => ClassSubjectType.English;

}
