using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "课件画布", menuName = "单例SO/课件画布")]
public class CWCanvas : SingletonSO<CWCanvas>
{

    [SerializeField]
    private GameObject canvasPrefab;

    private Canvas _canvas;


    private Canvas canvas
    {
        get
        {
            if (_canvas != null)
            {
                return _canvas;
            }

            var gb = Instantiate(canvasPrefab);

            _canvas = gb.GetComponent<Canvas>();

          

        

            return _canvas;

        }
    }


    public Canvas Init(Camera camera)
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        if (camera != null)
        {
            canvas.worldCamera = camera;
        }

        return canvas;
    }


}
