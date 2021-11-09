using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplayOnGUI : MonoBehaviour
{

    public int fpsTarget;
    public float updateInterval = 0.5f;
    private float lastInterval;
    private int frames = 0;
    private float fps;


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
    }
    void OnGUI()
    {
        var a = new GUIStyle();
        a.normal.textColor = Color.black;
        GUI.Label(new Rect(200, 40, 100, 30), fps.ToString(), a);
    }

}
