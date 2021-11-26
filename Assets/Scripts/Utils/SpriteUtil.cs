using System;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public enum LoadImageSizeType
{
    //此时width height不生效
    [LabelText("gameobject本身尺寸")]
    GameObjectSize,
    //此时width height不生效
    [LabelText("图片尺寸")]
    ImageSize,
    //此时width height不生效
    [LabelText("按gameobject宽度保留图片比例")]
    AdapteToGameObjectWidth,
    //此时width height不生效
    [LabelText("按gameobject高度保留图片比例")]
    AdaptedToGameObjectHeight,
    //此时仅width生效
    [LabelText("固定宽度并保留图片比例")]
    AdapteToWidth,
    //此时仅height生效
    [LabelText("固定高度并保留图片比例")]
    AdapteToHeight,
    //此时width height皆生效
    [LabelText("自定义宽高")]
    Fixed
}
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
        loadImageToSprite(url, sprite, LoadImageSizeType.GameObjectSize, 0f, 0f, anchor, 0.3f, then);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height)
    {
        loadImageToSprite(url, sprite, width, height, null);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height, Action then)
    {
        loadImageToSprite(url, sprite, LoadImageSizeType.Fixed, width, height, new Vector2(0.5f, 0.5f), 0.3f, then);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, float width, float height, Vector2 anchor, Action then)
    {
        loadImageToSprite(url, sprite, LoadImageSizeType.Fixed, width, height, anchor, 0.3f, then);
    }

    public static void loadImageToSprite(string url, SpriteRenderer sprite, LoadImageSizeType loadImageSizeType, float width, float height, Vector2 anchor, float duration, Action then)
    {
        WebReqeust.GetTexture(url,
       (result) =>
       {
           Color color = sprite.color;
           if (duration > 0) { sprite.color = new Color(0f, 0f, 0f, 0f); }

           float originalWidth = 0f;
           float originalHeight = 0f;
           if (null != sprite.sprite)
           {
               originalWidth = sprite.sprite.bounds.size.x * sprite.transform.localScale.x;
               originalHeight = sprite.sprite.bounds.size.y * sprite.transform.localScale.y;
           }
           TweenCallback afterImageLoad = () =>
           {
               //替换sprite 
               sprite.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), anchor);
               //修改大小
               switch (loadImageSizeType)
               {
                   case LoadImageSizeType.GameObjectSize:
                       sprite.transform.localScale = new Vector3(originalWidth * 100f / result.width, originalHeight * 100f / result.height, sprite.transform.localScale.z);
                       break;
                   case LoadImageSizeType.ImageSize:
                       break;
                   case LoadImageSizeType.AdapteToGameObjectWidth:
                       sprite.transform.localScale = new Vector3(originalWidth * 100f / result.width, originalWidth * 100f / result.width , sprite.transform.localScale.z);
                       break;
                   case LoadImageSizeType.AdaptedToGameObjectHeight:
                       sprite.transform.localScale = new Vector3(originalHeight * 100f / result.height , originalHeight * 100f / result.height, sprite.transform.localScale.z);
                       break;
                   case LoadImageSizeType.AdapteToWidth:
                       sprite.transform.localScale = new Vector3(width * 100f / result.width, width * 100f / result.width, sprite.transform.localScale.z);
                       break;
                   case LoadImageSizeType.AdapteToHeight:
                       sprite.transform.localScale = new Vector3(height * 100f / result.height , height * 100f / result.height, sprite.transform.localScale.z);
                       break;
                   case LoadImageSizeType.Fixed:
                       sprite.transform.localScale = new Vector3(width * 100f / result.width, height * 100f / result.height, sprite.transform.localScale.z);
                       break;
               }


               if (null != then) { then(); }
               if (duration > 0) { sprite.DOColor(color, duration); }
           };
           //渐隐
           //sprite.DOColor(new Color(0f, 0f, 0f, 0f), 0.3f).OnComplete(afterImageLoad) ;
           afterImageLoad();
       }, (msg) =>
       {
           Debug.Log("load image failed:" + msg);
       });
    }

    public static void loadImageToUIImage(string url, Image imageObject, LoadImageSizeType loadImageSizeType, float width, float height, Vector2 anchor, float duration, Action then)
    {
        WebReqeust.GetTexture(url,
       (result) =>
       {
           Color color = imageObject.color;
           if (duration > 0) {
               imageObject.color = new Color(0f, 0f, 0f, 0f);
           }

           float originalWidth = imageObject.rectTransform.sizeDelta.x;
           float originalHeight = imageObject.rectTransform.sizeDelta.y;
       
           TweenCallback afterImageLoad = () =>
           {
               //替换sprite 
               imageObject.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), anchor);
               //修改大小
               float imageRatio =(float)result.height / (float)result.width;
               Logging.Log("image width:"+result.width+" ,height:"+result.height+" ,ratio:"+imageRatio);
               switch (loadImageSizeType)
               {
                   case LoadImageSizeType.GameObjectSize:
                       break;
                   case LoadImageSizeType.ImageSize:
                       imageObject.rectTransform.sizeDelta = new Vector2(result.width,result.height);
                       break;
                   case LoadImageSizeType.AdapteToGameObjectWidth:
                       imageObject.rectTransform.sizeDelta = new Vector3(originalWidth , originalWidth * imageRatio);
                       break;
                   case LoadImageSizeType.AdaptedToGameObjectHeight:
                       imageObject.rectTransform.sizeDelta = new Vector3(originalHeight/imageRatio, originalHeight);
                       break;
                   case LoadImageSizeType.AdapteToWidth:
                       imageObject.rectTransform.sizeDelta = new Vector3(width, width*imageRatio);
                       break;
                   case LoadImageSizeType.AdapteToHeight:
                       imageObject.rectTransform.sizeDelta = new Vector3(height/imageRatio,height);
                       break;
                   case LoadImageSizeType.Fixed:
                       imageObject.rectTransform.sizeDelta = new Vector3(width, height);
                       break;
               }


               if (null != then) { then(); }
               if (duration > 0) { imageObject.DOColor(color, duration); }
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
