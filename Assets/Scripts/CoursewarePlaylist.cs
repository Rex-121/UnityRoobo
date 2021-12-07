using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;
using System;
public class CoursewarePlaylist : CoursewareBasicPlaylist
{

    public Image spriteRendener;

    public override bool SupportRountType(RoundIsPlaying.Type roundType) { return roundType == RoundIsPlaying.Type.picture || roundType == RoundIsPlaying.Type.pause; }

    IDisposable d;

    public override void RoundDidChanged()
    {
        base.RoundDidChanged();

        spriteRendener.gameObject.SetActive(true);

        d?.Dispose();
        d = Storage.GetTexture(new Parcel(round.src)).Subscribe((texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            spriteRendener.sprite = tempSprite;

        }, e => { }).AddTo(this);
    }

    public override void ClearStage()
    {
        base.ClearStage();

        spriteRendener.gameObject.SetActive(false);
    }

}
