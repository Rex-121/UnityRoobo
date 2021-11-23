using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BestHTTP;
using UnityEngine.Networking;
using System.Diagnostics;

using UniRx;

public class LoadAssetBundle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Logging.Log(Application.persistentDataPath);

        // Un

        //DownloadHandlerAssetBundle.GetContent


        Observable.Timer(System.TimeSpan.FromSeconds(10)).Subscribe(_ =>
        {
            StartCoroutine(StartXXX());
        }).AddTo(this);
        
    }


    IEnumerator StartXXX()
    {

 

        //第三种加载方式   使用UnityWbRequest  服务器加载使用http本地加载使用file
        string uri = @"file://" + Application.persistentDataPath + "/scene/abc.ab";
        //string uri = @"http://localhost/AssetBundles\model.ab";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle("https://dwn.roobo.com/apps/zhixueyuan/dev/pudding/pudding/4.2.2/abc.ab");
        yield return request.SendWebRequest();

        Stopwatch sw = new Stopwatch();

        sw.Start();

        Logging.Log("开始加载" + sw.ElapsedMilliseconds);

        Logging.Log("加载完成 " + sw.ElapsedMilliseconds);
        
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);

        Logging.Log("读取完成 " + sw.ElapsedMilliseconds);


        //使用里面的资源
        Object[] obj = ab.LoadAllAssets<GameObject>();//加载出来放入数组中
        // 创建出来
        foreach (Object o in obj)
        {
            Instantiate(o);
            
            Logging.Log("ins完成 " + sw.ElapsedMilliseconds);
        }

        sw.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
