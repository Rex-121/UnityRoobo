using UnityEngine;

[CreateAssetMenu(fileName = "美术", menuName = "学科/美术")]
public class ArtSubject : ClassSubject
{
    public override ClassSubjectType type => ClassSubjectType.Art;
}
