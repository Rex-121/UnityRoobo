using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof(Image))]
public class CW_JustPause : CoursewarePlayer
{

    Image image;

    public void SetImageData(CW_JustPause_SO.Image i)
    {
        Storage.GetTexture(new Parcel(i.image)).Subscribe((texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            image.sprite = tempSprite;

        }, e => { }).AddTo(this);
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Next()
    {
        DidEndCourseware(this);
    }
}
