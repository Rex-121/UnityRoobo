using UnityEngine;

[CreateAssetMenu(fileName = "音乐", menuName = "学科/音乐")]
public class MusicSubject : ClassSubject
{
    public override ClassSubjectType type => ClassSubjectType.Music;
}
