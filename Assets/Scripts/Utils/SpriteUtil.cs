using System;
using UnityEngine;
using DG.Tweening;

public class SpriteUtil
{
    public static void loadImageToSprite(string url, SpriteRenderer sprite)
    {
        loadImageToSprite(url, sprite, new Vector2(0.5f, 0.5f));
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, Vector2 anchor)
    {
        loadImageToSprite(url, sprite, anchor, null);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, Vector2 anchor, Action then)
    {
        float originalWidth = sprite.sprite.bounds.size.x;
        float originalHeight = sprite.sprite.bounds.size.y;
        loadImageToSprite(url, sprite, originalWidth, originalHeight, anchor, then);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height)
    {
        loadImageToSprite(url, sprite, width, height, null);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height, Action then)
    {
        loadImageToSprite(url, sprite, width, height, new Vector2(0.5f, 0.5f), then);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height, Vector2 anchor, Action then)
    {
        WebReqeust.GetTexture(url,
       (result) =>
       {
           Color color = sprite.color;
           sprite.color = new Color(0f,0f,0f,0f);
           TweenCallback afterImageLoad = () =>
           {
               //替换sprite 
               sprite.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), anchor);
               //修改大小
               sprite.transform.localScale = new Vector3(width * 100f / result.width, height * 100f / result.height, sprite.transform.localScale.z);
               if (null != then) { then(); }
               sprite.DOColor(color, 0.5f);
           };
           //渐隐
           //sprite.DOColor(new Color(0f, 0f, 0f, 0f), 0.3f).OnComplete(afterImageLoad) ;
           afterImageLoad();
       }, (msg) =>
       {
           Debug.Log("load image failed:" + msg);
       });
    }
}
