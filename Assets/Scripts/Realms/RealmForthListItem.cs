using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class RealmForthListItem : MonoBehaviour
{


    Round round;

    public Image image;

    public Text label;

    IDisposable dis;

    public void SetRound(Round round)
    {
        this.round = round;

        if (!string.IsNullOrEmpty(this.round.icon))
        {
            dis = Storage.GetImage(new Parcel(this.round.icon)).Subscribe(sprite =>
            {
                image.sprite = sprite;
            }).AddTo(this);
        }




        label.text = this.round.name;

    }

    private void OnDisable()
    {
        dis?.Dispose();
    }

}
