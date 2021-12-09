using System.Collections.Generic;
using UnityEngine;

using BestHTTP;
using System;


public class HttpRaw
{


    static HTTPRequest AddHeaderFor(HTTPRequest request, Dictionary<string, string> headers)
    {
        if (headers == null) return request;

        foreach (KeyValuePair<string, string> keyValue in headers)
        {
            request.AddHeader(keyValue.Key, keyValue.Value);
        }

        return request;
    }



    public static void Reqeust(Uri uri, HTTPMethods methods, Dictionary<string, string> headers, object data, Action<HTTPResponse> success, Action<HttpError> error)
    {
        switch (methods)
        {
            case HTTPMethods.Get:
                Get(uri, headers, success, error);
                return;
            case HTTPMethods.Post:
                Post(uri, headers, data, success, error);
                return;
        }

        error(new HttpError(-999, "尚不支持 " + methods.ToString(), HttpError.Type.Business));
    }





    public static void Post(Uri uri, Dictionary<string, string> headers, object data, Action<HTTPResponse> success, Action<HttpError> error)
    {

        Logging.Log(success);
        Logging.Log(error);
        HTTPRequest requestx;
        requestx = new HTTPRequest(uri, HTTPMethods.Post, (r, re) =>
        {
            SelectData(r, re, success, error);
        });

        AddHeaderFor(requestx, headers);

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


    public static void Get(Uri uri, Dictionary<string, string> headers, Action<HTTPResponse> success, Action<HttpError> error)
    {

        HTTPRequest requestx;
        requestx = new HTTPRequest(uri, HTTPMethods.Get, (r, re) =>
        {
            SelectData(r, re, success, error);
        });

        AddHeaderFor(requestx, headers);

        requestx.Send();
    }



    public static void GetResource(string path, Action<HTTPResponse> success, Action<HttpError> error)
    {


        HTTPRequest requestx;
        requestx = new HTTPRequest(new Uri(path), HTTPMethods.Get, (r, re) =>
        {
            SelectData(r, re, success, error);
        });

        requestx.DisableCache = false;

        requestx.Send();
    }


    static void SelectData(HTTPRequest request, HTTPResponse response, Action<HTTPResponse> success, Action<HttpError> error)
    {
        if (response == null)
        {
            error(new HttpError(-8888, "", HttpError.Type.HTTP));
            return;
        }

        if (response.StatusCode == 200)
        {
            success(response);
        }
        else
        {
            Logging.Log("3");
            if (response.StatusCode == 304 && !request.DisableCache)
            {
                success(response);
            }
            else
            {
                error(new HttpError(response.StatusCode, response.Message, HttpError.Type.HTTP));
            }

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