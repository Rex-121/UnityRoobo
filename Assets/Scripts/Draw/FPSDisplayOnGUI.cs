using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplayOnGUI : MonoBehaviour
{

    public int fpsTarget;
    public float updateInterval = 0.5f;
    private float lastInterval;
    private int frames = 0;
    private float fps;

    public TextMeshProUGUI text;

    private void Start()
    {

        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

    }

    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow >= lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }

        if (text != null)
            text.text = fps.ToString("0");
    }

    //void OnGUI()
    //{
    //    var a = new GUIStyle();
    //    a.normal.textColor = Color.black;
    //    GUI.Label(new Rect(200, 40, 100, 30), fps.ToString(), a);
    //}

}
