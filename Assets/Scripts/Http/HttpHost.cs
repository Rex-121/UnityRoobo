using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HttpHost", menuName = "单例SO/HttpHost")]
public class HttpHost : SingletonSO<HttpHost>
{

    public string host = "http://appcourse.roobo.com.cn";

    public Dictionary<string, string> defaultHeaders = new Dictionary<string, string>();


}
