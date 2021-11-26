using UnityEngine;

[CreateAssetMenu(fileName = "美术", menuName = "学科/美术")]
public class ArtSubject : ClassSubject
{
    public override Type type => Type.Art;

    public override string fileName => "美术";
}
