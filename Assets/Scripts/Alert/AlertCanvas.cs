using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlertCanvas : MonoBehaviour
{
    public GameObject mask;

    public GameObject commonAlertPrefab;
    public GameObject scrollAlertPrefab;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        CommonScrollAlert("厉害啊啊啊", "空气喝个我哦起文化宫皮还欠我个IQ我我欧巴歌起舞不公平为确保工期微博过去哦IG抱歉哦我我不够切割我我去玩爆破", (a) => {
            if (a)
            {
                Debug.Log("YES");
            }
            else
            {
                Debug.Log("NO");
            }
        });
    }

    public void CommonScrollAlert(string title, string content, Action<bool> callback)
    {
        var custom = Instantiate(commonAlertPrefab, mask.transform).GetComponent<CommonAlert>();
        custom.SetTitle(title);
        custom.SetContent(content);
        custom.callback = callback;
        custom.Present();
    }

    public void ShowScrollAlert(string title, string content, Action<bool> callback)
    {
        var custom = Instantiate(scrollAlertPrefab, mask.transform).GetComponent<CommonAlert>();
        custom.SetTitle(title);
        custom.SetContent(content);
        custom.callback = callback;
        custom.Present();
    }


}
