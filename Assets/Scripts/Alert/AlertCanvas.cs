using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlertCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject mask;

    [SerializeField]
    private GameObject commonAlertPrefab;
    [SerializeField]
    private GameObject scrollAlertPrefab;
    [SerializeField]
    private GameObject networkAlertPrefab;


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
        ShowNetworkAlert((a) => {
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

    //通用弹窗
    public void ShowCommonAlert(string title, string content)
    {
        ShowCommonAlert(title, content, null);
    }

    public void ShowCommonAlert(string title, string content, Action<bool> callback)
    {
        var custom = Instantiate(commonAlertPrefab, mask.transform).GetComponent<CommonAlert>();
        custom.SetTitle(title);
        custom.SetContent(content);
        custom.callback = callback;
        custom.Present();
    }

    //滚动内容弹窗
    public void ShowScrollAlert(string title, string content)
    {
        ShowScrollAlert(title, content, null);
    }

    public void ShowScrollAlert(string title, string content, Action<bool> callback)
    {
        var custom = Instantiate(scrollAlertPrefab, mask.transform).GetComponent<CommonAlert>();
        custom.SetTitle(title);
        custom.SetContent(content);
        custom.callback = callback;
        custom.Present();
    }

    //网络问题弹窗
    public void ShowNetworkAlert()
    {
        ShowNetworkAlert(null);
    }

    public void ShowNetworkAlert(Action<bool> callback)
    {
        var custom = Instantiate(networkAlertPrefab, mask.transform).GetComponent<CommonAlert>();
        custom.callback = callback;
        custom.Present();
    }

}
