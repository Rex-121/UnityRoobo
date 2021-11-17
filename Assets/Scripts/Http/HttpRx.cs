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

                }
                catch
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

    static IObservable<T> RawGet<T>(string path)
    {
        Stopwatch sw = new Stopwatch();
        return Observable.Create<T>((ob) =>
        {
            HttpRaw.Get(path, (r) =>
            {
                try
                {
                    var data = JsonUtility.FromJson<HttpRaw.Data<T>>(r.DataAsText);
                    ob.OnNext(data.data);

                }
                catch
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

    static IObservable<byte[]> RawGetResource(string path)
    {
        return Observable.Create<byte[]>((ob) =>
        {
            HttpRaw.GetResource(path, (r) =>
            {
                try
                {

                    ob.OnNext(r.Data);

                }
                catch
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

    public static IObservable<byte[]> GetResource(string path)
    {
        return RawGetResource(path);
    }


    public static IObservable<AudioClip> GetAudio(string path)
    {
        return Observable.Create<AudioClip>((ob) =>
      {
          WebReqeust.GetAudio(path, (clip) =>
          {
              ob.OnNext(clip);
              ob.OnCompleted();
          }, (e) =>
          {
              ob.OnError(new HttpError(-9922, e, HttpError.Type.Business));
          });
          return null;
      });
    }

    [Serializable]
    public struct Ignore
    {

    }

}
