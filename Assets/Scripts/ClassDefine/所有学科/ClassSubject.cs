using UnityEngine;
using Sirenix.OdinInspector;

public abstract class ClassSubject: ScriptableObject 
{


    [ShowInInspector]
    public abstract ClassSubjectType type { get; }

    [LabelText("主题颜色")]
    public Color theme;



}


public enum ClassSubjectType
{

    Art, Language, Music

}


public static class ClassSubjectExtension
{
    public static string SO文件名(this ClassSubjectType o)
    {
        switch (o)
        {
            case ClassSubjectType.Art: return "美术";
            case ClassSubjectType.Language: return "语言";
            case ClassSubjectType.Music: return "音乐";
        }
        return "";
    }

    public static string SO文件名(this ClassSubjectType? o)
    {
        if (o == null) return "";
        return SO文件名((ClassSubjectType)o);
    }
}