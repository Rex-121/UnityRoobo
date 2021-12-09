using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="画板载体图",menuName = "课件/配置/画板载体图")]
public class CW_PictureChoice_Carried_SO : SerializedScriptableObject
{
    public struct Config
    {
        [LabelText("载体图片"), PreviewField(Height = 50)]
        public Sprite carrier;

        [LabelText("遮罩边框图片"), PreviewField(Height = 50)]
        public Sprite mask;

        [LabelText("载体图片居上距离")]
        public float carrierPosY;

        [LabelText("间距")]
        public Dictionary<int, float> dic;
    }

  public  Dictionary<CW_PictrueBoardChoice_SO.PictureBoardEntity.QuestionStyle, Config> dic = new Dictionary<CW_PictrueBoardChoice_SO.PictureBoardEntity.QuestionStyle, Config>();

}
