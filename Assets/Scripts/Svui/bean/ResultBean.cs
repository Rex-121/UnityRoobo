using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class BaseResultBean<T> 
{
    public int id;
    public bool isSuccess;
    public string methodName;
    public T data;
}

public class OralResultBean
{
    public string recordPath;
    public string word;
    public int score;
    public List<WordsBean> words;
}

public class WordsBean {
    public int beginTime;
    public int endTime;
    public string word;
    public ScoresBean scores;
}

public class ScoresBean {
    public int pronunciation;
}

public class QAResultBean
{
    public AiBean ai;
    public string apiVersion;
    public ASRBean asr;
    public string recordPath;
    public StatusBean status;
    public string traceId;
}

public class AiBean {
    public string query;
    public SemanticBean semantic;
    public StatusBean status;
}

public class SemanticBean {
    public string action;
    public OutputContextBean outputContext;
    public string service;
}

public class OutputContextBean {
    public string context;
    public string service;
}

public class ASRBean {
    public string text;
}

public class StatusBean {
    public int code;
    public string errorType;
}