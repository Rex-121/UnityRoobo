using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class NativeDataDeliver<T>
{

    public string type;

    public T data;

}

[Serializable]
public class SvuiData : NativeDataDeliver<FollowReadData>
{

    public SvuiData(FollowReadData data)
    {
        this.type = "Svui";
        this.data = data;
    }
}


[Serializable]
public class FollowReadData
{
    public string audio = "https://roobo-test.oss-cn-beijing.aliyuncs.com/appcourse/manager/2021-07-13/c3ml5t0rjdcmt7uaegqg.mp3";

    public string text = "";
}
