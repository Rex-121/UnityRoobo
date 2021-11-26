using UnityEngine;
using Sirenix.OdinInspector;

public abstract class ClassSubject: ScriptableObject 
{


    [ShowInInspector]
    public abstract Type type { get; }


    [ShowInInspector]
    public abstract string fileName { get; }


    public enum Type
    {

        Art, Language

    }

}
