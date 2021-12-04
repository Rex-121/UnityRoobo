using System;
using Newtonsoft.Json;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SvuiBridge", menuName = "单例SO/SvuiBridge")]
public class SvuiBridge : SingletonSO<SvuiBridge>
{
    public enum Language
    {
        AUTO,
        CN,
        JP,
        ENG,
    }

    private int id = 0;
    Dictionary<int, BehaviorSubject<bool>> initCallMap = new Dictionary<int, BehaviorSubject<bool>>();
    Dictionary<int, BehaviorSubject<OralResultBean>> oralCallMap = new Dictionary<int, BehaviorSubject<OralResultBean>>();
    Dictionary<int, BehaviorSubject<QAResultBean>> qaCallMap = new Dictionary<int, BehaviorSubject<QAResultBean>>();
    Dictionary<int, BehaviorSubject<string>> ttsCallMap = new Dictionary<int, BehaviorSubject<string>>();


    public BehaviorSubject<bool> initSvui(string appPackageId, string appToken, string appUid, bool isTest)
    {
        InitParamsBean paramsBean = new InitParamsBean();
        id++;
        paramsBean.id = id;
        paramsBean.appPackageId = appPackageId;
        paramsBean.appToken = appToken;
        paramsBean.appUId = appUid;
        paramsBean.isTest = isTest;

        string json = JsonConvert.SerializeObject(paramsBean);
        callNative("initSvui", json);

        BehaviorSubject<bool> resultSubject = new BehaviorSubject<bool>(false);
        initCallMap.Add(id, resultSubject);
        return resultSubject;
    }

    public BehaviorSubject<OralResultBean> oralEvaluate(string text)
    {
        return oralEvaluate(text, Language.AUTO);
    }

    public BehaviorSubject<OralResultBean> oralEvaluate(string text, Language language)
    {
        return oralEvaluate(text, language, false);
    }

    public BehaviorSubject<OralResultBean> oralEvaluate(string text, Language language, bool isManual)
    {
        return oralEvaluate(text, language, isManual, 5);
    }

    public BehaviorSubject<OralResultBean> oralEvaluate(string text, Language language, bool isManual, int delayFrame)
    {
        string lan = "";
        switch (language)
        {
            case Language.AUTO:
                lan = "";
                break;
            case Language.CN:
                lan = "zh";
                break;
            case Language.ENG:
                lan = "en";
                break;
            case Language.JP:
                lan = "jp";
                break;
        }
        OralParams oralParams = new OralParams();
        id++;
        oralParams.id = id;
        oralParams.text = text;
        oralParams.language = lan;
        oralParams.isManual = isManual;
        oralParams.delayFrame = delayFrame;
        string json = JsonConvert.SerializeObject(oralParams);
        callNative("oralEvaluate", json);

        BehaviorSubject<OralResultBean> resultSubject = new BehaviorSubject<OralResultBean>(null);
        oralCallMap.Add(id, resultSubject);
        return resultSubject;
    }

    public BehaviorSubject<QAResultBean> startQA(int lessonId, string dialogId)
    {
        return startQA(lessonId, dialogId, 5);
    }

    public BehaviorSubject<QAResultBean> startQA(int lessonId, string dialogId, int delayFrame)
    {
        QAParamsBean qaParams = new QAParamsBean();
        id++;
        qaParams.id = id;
        qaParams.lessionId = lessonId;
        qaParams.dialogId = dialogId;
        qaParams.delayFrame = delayFrame;
        string json = JsonConvert.SerializeObject(qaParams);
        callNative("startQa", json);

        BehaviorSubject<QAResultBean> resultSubject = new BehaviorSubject<QAResultBean>(null);
        qaCallMap.Add(id, resultSubject);
        return resultSubject;
    }

    public void stopCapture()
    {
        callNative("stopCapture");
    }

    public BehaviorSubject<string> tts(string text)
    {
        return tts(text, Language.AUTO);
    }

    public BehaviorSubject<string> tts(string text, Language language)
    {
        string lan = "";
        switch (language)
        {
            case Language.AUTO:
                lan = "";
                break;
            case Language.CN:
                lan = "zh";
                break;
            case Language.ENG:
                lan = "en";
                break;
            case Language.JP:
                lan = "jp";
                break;
        }
        TTSParamsBean ttsParams = new TTSParamsBean();
        id++;
        ttsParams.id = id;
        ttsParams.text = text;
        ttsParams.language = lan;
        string json = JsonConvert.SerializeObject(ttsParams);
        callNative("tts", json);

        BehaviorSubject<string> resultSubject = new BehaviorSubject<string>(null);
        ttsCallMap.Add(id, resultSubject);
        return resultSubject;
    }

    public void clearAudioRecord()
    {
        callNative("clearAudioRecord");
    }

    public long getAudioRecordSize()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.roobo.aiclasslibrary.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");
            return overrideActivity.Call<long>("getAudioRecordSize");
        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
            return 0;
        }
#endif
    }

    public void clearPcm()
    {
        callNative("clearPcm");
    }

    public void release()
    {
        callNative("release");
    }

    private void callNative(string method)
    {
        callNative(method, "");
    }

    private void callNative(string method, string json)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.roobo.aiclasslibrary.OverrideUnityActivity");
            AndroidJavaObject overrideActivity = jc.GetStatic<AndroidJavaObject>("instance");

            Logging.Log("json:" + json);
            if (json == null || json == "")
            {
                overrideActivity.Call(method);
            }
            else
            {
                overrideActivity.Call(method, json);
            }
        }
        catch (Exception e)
        {
            Logging.Log(e.Message);
        }
#endif
    }

    public void DidReceiveFromNative(string json)
    {
        BaseResultBean<object> resultbean = JsonConvert.DeserializeObject<BaseResultBean<object>>(json);
        if (resultbean.isSuccess)
        {
            switch (resultbean.methodName)
            {
                case "initSvui":
                    BehaviorSubject<bool> initBs = initCallMap[resultbean.id];
                    initBs.OnNext(true);
                    initBs.OnCompleted();
                    initCallMap.Remove(resultbean.id);
                    break;
                case "oralEvaluate":
                    BehaviorSubject<OralResultBean> bs = oralCallMap[resultbean.id];
                    bs.OnNext(JsonConvert.DeserializeObject<OralResultBean>(resultbean.data.ToString()));
                    bs.OnCompleted();
                    oralCallMap.Remove(resultbean.id);
                    break;
                case "startQa":
                    BehaviorSubject<QAResultBean> qaBs = qaCallMap[resultbean.id];
                    qaBs.OnNext(JsonConvert.DeserializeObject<QAResultBean>(resultbean.data.ToString()));
                    qaBs.OnCompleted();
                    qaCallMap.Remove(resultbean.id);
                    break;
                case "tts":
                    BehaviorSubject<string> ttsBs = ttsCallMap[resultbean.id];
                    ttsBs.OnNext(resultbean.data.ToString());
                    ttsBs.OnCompleted();
                    ttsCallMap.Remove(resultbean.id);
                    break;
            }
        }
        else
        {
            Logging.Log("native call failed:" + resultbean.data);
            switch (resultbean.methodName)
            {
                case "initSvui":
                    BehaviorSubject<bool> initBs = initCallMap[resultbean.id];
                    initBs.OnError(new Exception(resultbean.data.ToString()));
                    initCallMap.Remove(resultbean.id);
                    break;
                case "oralEvaluate":
                    BehaviorSubject<OralResultBean> bs = oralCallMap[resultbean.id];
                    bs.OnError(new Exception(resultbean.data.ToString()));
                    oralCallMap.Remove(resultbean.id);
                    break;
                case "startQa":
                    BehaviorSubject<QAResultBean> qaBs = qaCallMap[resultbean.id];
                    qaBs.OnError(new Exception(resultbean.data.ToString()));
                    qaCallMap.Remove(resultbean.id);
                    break;
                case "tts":
                    BehaviorSubject<string> ttsBs = ttsCallMap[resultbean.id];
                    ttsBs.OnError(new Exception(resultbean.data.ToString()));
                    ttsCallMap.Remove(resultbean.id);
                    break;
            }
        }
    }
}
