using System;
using Newtonsoft.Json;

public class BaseParamsBean
{
    public int id;
}

public class OralParams : BaseParamsBean
{
    public string text;
    public string language;
    public bool isManual;
    public int delayFrame;
}

public class InitParamsBean : BaseParamsBean
{
    public String appPackageId;

    public String appToken;

    public String appUId;

    public bool isTest;
}

public class QAParamsBean : BaseParamsBean {
    public int lessionId;
    public string dialogId;
    public int delayFrame;
}

public class TTSParamsBean : BaseParamsBean
{
    public string text;
    public string language;
}