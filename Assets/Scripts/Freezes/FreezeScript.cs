using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : CoursewarePlayer
{

    public GameObject prefabImage;
    // Start is called before the first frame update
    void Start()
    {
        var gameObject=Instantiate(prefabImage, transform);
        var compument = gameObject.GetComponent<FreezeImageView>();
        List<string> urls = new List<string>();

        urls.Add("https://img1.baidu.com/it/u=593629569,3400129487&fm=26&fmt=auto");
        urls.Add("https://img2.baidu.com/it/u=2128531977,1485809527&fm=26&fmt=auto");
        urls.Add("https://img2.baidu.com/it/u=2904726381,1561159801&fm=26&fmt=auto");
        urls.Add("https://img1.baidu.com/it/u=3982915423,2844991468&fm=26&fmt=auto");

        urls.Add("https://img0.baidu.com/it/u=1108252134,4114637978&fm=26&fmt=auto");
        //urls.Add("https://img1.baidu.com/it/u=1534625518,86228024&fm=26&fmt=auto");
        //urls.Add("https://img0.baidu.com/it/u=3927459320,2138990686&fm=26&fmt=auto");
        //urls.Add("https://img1.baidu.com/it/u=593629569,3400129487&fm=26&fmt=auto");

        //urls.Add("https://img1.baidu.com/it/u=1634731084,2544465278&fm=26&fmt=auto");


        compument.InitGrids(urls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
}
