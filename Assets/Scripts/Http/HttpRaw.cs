using System.Collections.Generic;
using UnityEngine;

using BestHTTP;
using System;


public class HttpRaw
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
    public struct RawData
    {
        public string msg;
        public int result;
        public string desc;

        public bool success => result == 0;


        //public string JSON;
    }


    static HTTPRequest AddHeaderFor(HTTPRequest request)
    {
        var headers = HttpHost.Default.defaultHeaders;

        foreach (KeyValuePair<string, string> keyValue in headers)
        {
            request.AddHeader(keyValue.Key, keyValue.Value);
        }

        return request;
    }

    static Uri BuildPath(string path)
    {
        return new Uri(HttpHost.Default.host + "/" + path);
    }

    public static void Post(string path, object data, Action<HTTPResponse> success, Action<HttpError> error)
    {


        HTTPRequest requestx;
        requestx = new HTTPRequest(BuildPath(path), HTTPMethods.Post, (r, re) =>
        {
            SelectData(re, success, error);
        });

        AddHeaderFor(requestx);

        if (data != null)
        {
            try
            {
                var s = JsonUtility.ToJson(data);

                requestx.RawData = System.Text.Encoding.UTF8.GetBytes(s);

                requestx.Send();
            }
            catch (Exception e)
            {
                error(HttpError.InDataError(e));
            }
        }
        else
        {
            requestx.Send();
        }

    }


    static void SelectData(HTTPResponse response, Action<HTTPResponse> success, Action<HttpError> error)
    {
        if (response.StatusCode == 200)
        {
            success(response);
        }
        else
        {
            error(new HttpError(response.StatusCode, response.Message, HttpError.Type.HTTP));
        }
    }

}



public class HttpError : ApplicationException
{
    public int code;

    public string message;


    public enum Type
    {
        HTTP, Business
    }

    public Type type;

    public Exception exception;

    public HttpError(int c, string m, Type t, Exception e)
    {
        code = c;
        message = m;
        exception = e;
        type = t;
    }

    public HttpError(int c, string m, Type t)
    {
        code = c;
        message = m;
        exception = null;
        type = t;
    }

    /// <summary>
    /// 解析错误
    /// </summary>
    public static HttpError ParseError = new HttpError(-8789, "解析错误", Type.Business);



    /// <summary>
    /// 入参失败
    /// </summary>
    public static HttpError InDataError(Exception e)
    {
        return new HttpError(-8788, "入参失败", Type.Business);
    }

}