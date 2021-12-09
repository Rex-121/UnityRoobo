
using UnityEngine;
using UniRx;

public class Storage
{


    public static System.IObservable<AudioClip> GetAudio(Parcel parcel)
    {
        return HttpRx.GetAudio(parcel.truePath);
    }



    public static System.IObservable<Texture2D> GetTexture(Parcel parcel)
    {
        return HttpRx.GetTexture2D(parcel.truePath);
    }


    public static System.IObservable<Sprite> GetImage(Parcel parcel)
    {
        return GetTexture(parcel).Select(texture =>
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        });
    }

    public static System.IObservable<Sprite> GetImage(Parcel parcel, Rect rect, Vector2 pivot)
    {
        return GetTexture(parcel).Select(texture =>
        {
            return Sprite.Create(texture, rect, pivot);
        });
    }


}
