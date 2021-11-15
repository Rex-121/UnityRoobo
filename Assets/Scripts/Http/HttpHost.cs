using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "HttpHost", menuName = "单例SO/HttpHost")]
public class HttpHost : SingletonSO<HttpHost>
{

    public static HttpHost Default => Instance("HttpHost");

    public string host = "http://appcourse.roobo.com.cn";

    public Dictionary<string, string> defaultHeaders = new Dictionary<string, string>();


}
