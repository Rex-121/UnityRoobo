using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class AIDraw : MonoBehaviour
{
    [ShowInInspector]
    [LabelText("Red")]
    public Image mRed;

    [ShowInInspector]
    [LabelText("Yellow")]
    public Image mYellow;

    [ShowInInspector]
    [LabelText("Green")]
    public Image mGreen;

    [ShowInInspector]
    [LabelText("Blue")]
    public Image mBlue;

    [ShowInInspector]
    [LabelText("Brown")]
    public Image mBrown;

    [ShowInInspector]
    [LabelText("Black")]
    public Image mBlack;

    [ShowInInspector]
    [LabelText("rawImage")]
    public RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        InitPoints();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private GameObject gameObject;
    private GameObject gameObject2;
    public void InitPoints()
    {


        //GameObject gameObject = new GameObject("newObject" + x + y);
        //var image = gameObject.AddComponent<Image>();
        //gameObject.transform.localPosition = new Vector3(x, y);
        //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //rectTransform.sizeDelta = new Vector2(50f, 50f);
        //gameObject.transform.SetParent(transform);

        for (int i = 1; i <= 6; i++)
        {
            PointsFactorys(i * 100, i * 100);
        }

        for (int i = 7; i <= 11; i++)
        {
            PointsFactorys(i * 100, i * 100 - 50 * i);
        }
    }


    private void PointsFactorys(float x, float y)
    {
        GameObject gameObject = new GameObject("newObject" + x + y);
        var image = gameObject.AddComponent<Image>();
        gameObject.transform.localPosition = new Vector3(x, y);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50f, 50f);
        gameObject.transform.SetParent(transform);
    }


    Texture2D texture;

    private void Awake()
    {
        texture = new Texture2D(Screen.width,Screen.height);
        rawImage.texture = texture;
        texture.SetPixel(500, 500, Color.blue);
        texture.Apply();
    }

    public void Clear()
    {
        for(int y= 0; y < texture.height; y++)
        {
            for(int x = 0; x < texture.width; x++)
            {
                Color color = Color.white;
                texture.SetPixel(x, y, color);
            }
        }
    }

}
