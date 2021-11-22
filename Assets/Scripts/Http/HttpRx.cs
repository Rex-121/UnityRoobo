using UnityEngine;

using UniRx;

using System.Diagnostics;

using System;
using System.Collections.Generic;
using System.Text;


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

    static Uri BuildPath(string path, Dictionary<string, string> query)
    {
        var build = new UriBuilder(HttpHost.Default.host + path);
        build.Query = UrlQuery.Make(query);
        return build.Uri;
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
            HttpRaw.Post(BuildPath(path, null), GetHeaders(), data, (r) =>
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

    static IObservable<T> RawGet<T>(string path, Dictionary<string, string> query)
    {
        return Observable.Create<T>((ob) =>
        {
            HttpRaw.Get(uri: BuildPath(path, query), headers: GetHeaders(), (r) =>
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

    /// <summary>
    /// GET
    /// </summary>
    /// <typeparam name="T">解析数据</typeparam>
    /// <param name="path">接口</param>
    /// <param name="query">query参数</param>
    /// <returns></returns>
    public static IObservable<T> Get<T>(string path, Dictionary<string, string> query)
    {
        return RawGet<T>(path, query);
    }

    /// <summary>
    /// GET
    /// </summary>
    /// <typeparam name="T">解析数据</typeparam>
    /// <param name="path">接口</param>
    /// <returns></returns>
    public static IObservable<T> Get<T>(string path)
    {
        return RawGet<T>(path, query: null);
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



class UrlQuery
{

    private const int EscapeTreshold = 256;


    public static string Make(Dictionary<string, string> parmas)
    {
        if (parmas == null) return "";
        
        string v = "";
        foreach (KeyValuePair<string, string> dic in parmas)
        {
            v += (EscapeString(dic.Key) + "=" + EscapeString(dic.Value));
            v += "&";
        }
        var index = v.LastIndexOf("&");
        v = v.Remove(index);

        Logging.Log("QUERY" + v);

        return v;
    }

    public static string EscapeString(string originalString)
    {
        if (originalString.Length < EscapeTreshold)
            return Uri.EscapeDataString(originalString);
        else
        {
            int loops = originalString.Length / EscapeTreshold;
            StringBuilder sb = new StringBuilder(loops);

            for (int i = 0; i <= loops; i++)
                sb.Append(i < loops ?
                             Uri.EscapeDataString(originalString.Substring(EscapeTreshold * i, EscapeTreshold)) :
                             Uri.EscapeDataString(originalString.Substring(EscapeTreshold * i)));
            return sb.ToString();
        }
    }

}