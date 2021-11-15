using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using BestHTTP;

using System.Diagnostics;

using System;



public class HttpRx
{
    static IObservable<T> RawPost<T>(string path, object data)
    {
        Stopwatch sw = new Stopwatch();
        return Observable.Create<T>((ob) =>
        {
            HttpRaw.Post(path, data, (r) =>
            {
                try
                {
                    var data = JsonUtility.FromJson<HttpRaw.Data<T>>(r.DataAsText);
                    ob.OnNext(data.data);
                    
                } catch
                {
                    ob.OnError(HttpError.ParseError);
                }
                finally
                {
                    ob.OnCompleted();
                }
            }, (e) =>
            {
                ob.OnError(e);
            });

            return null;
        });
    }


    public static IObservable<T> Post<T>(string path, object data)
    {
        return RawPost<T>(path, data);
    }

    public static IObservable<Ignore> Post(string path, object data)
    {
        return RawPost<Ignore>(path, data);
    }

    [Serializable]
    public struct Ignore
    {

    }

}
