using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeImageView : MonoBehaviour
{
    [ShowInInspector]
    [LabelText("图片")]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image.rectTransform.DOMove(new Vector2(100, 100), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
