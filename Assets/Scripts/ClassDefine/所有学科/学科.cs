using UnityEngine;
using Sirenix.OdinInspector;

public abstract class 学科: ScriptableObject 
{


    [ShowInInspector]
    public abstract 类型 type { get; }



    public enum 类型
    {

        美术, 语言, 音乐

    }

}


public static class 学科拓展
{
    public static string SO文件名(this 学科.类型 o)
    {
        switch (o)
        {
            case 学科.类型.美术: return "美术";
            case 学科.类型.语言: return "语言";
            case 学科.类型.音乐: return "音乐";
        }
        return "";
    }

    public static string SO文件名(this 学科.类型? o)
    {
        if (o == null) return "";
        return SO文件名((学科.类型)o);
    }
}