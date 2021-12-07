using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UniRx;
using System.Linq;
public class CoursewarePlaylist : CoursewareBasicPlaylist
{

    public Image spriteRendener;

    public override bool SupportRountType(RoundIsPlaying.Type roundType) { return roundType == RoundIsPlaying.Type.picture; }



    public override void RoundDidChanged()
    {
        base.RoundDidChanged();

        spriteRendener.gameObject.SetActive(true);

        WebReqeust.GetTexture(round.src, (texture2d) =>
        {
            Sprite tempSprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
            spriteRendener.sprite = tempSprite;

        }, (e) =>
        {
        });
    }

    public override void ClearStage()
    {
        base.ClearStage();

        spriteRendener.gameObject.SetActive(false);
    }

}
