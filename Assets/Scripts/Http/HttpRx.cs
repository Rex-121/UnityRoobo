using UnityEngine;

using UniRx;

using System.Diagnostics;

using System;
using System.Collections.Generic;

public class HttpRx
{

    [Serializable]
    public struct Data<T>
    {
        public T data;
        public string msg;
        public int result;
        public string desc;
    }

    [Serializable]
    public struct Ignore
    {

    }

    static Uri BuildPath(string path)
    {
        return new Uri(HttpHost.Default.host + path);
    }

    static Dictionary<string, string> GetHeaders()
    {
        return HttpHost.Default.defaultHeaders;
    }


    static IObservable<T> RawPost<T>(string path, object data)
    {
        Stopwatch sw = new Stopwatch();
        return Observable.Create<T>((ob) =>
        {
            HttpRaw.Post(BuildPath(path), GetHeaders(), data, (r) =>
            {
                try
                {
                    var data = JsonUtility.FromJson<Data<T>>(r.DataAsText);
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

    /// ------------------------------------------------------------------------

    static IObservable<T> RawGet<T>(string path)
    {
        return Observable.Create<T>((ob) =>
        {
            HttpRaw.Get(BuildPath(path), GetHeaders(), (r) =>
            {
                try
                {
                    Logging.Log(r.DataAsText);


                    var data = JsonUtility.FromJson<Data<T>>(r.DataAsText);
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


    public static IObservable<T> Get<T>(string path)
    {
        return RawGet<T>(path);
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



}
