using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "定格Player", menuName = "课件/Player/定格Player")]
public class CW_Freeze_SO : CoursewarePlayer_SO
{

    public override bool MakeData(GameObject player)
    {
        player.GetComponent<CW_Freeze>().InitGridAndData(item);
        return true;
    }

    public FreezeEntity item;
    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        Debug.Log("CW_Freeze_SO= " + content);
        var d = CreateInstance<CW_Freeze_SO>();
        d.coursewarePlayer = coursewarePlayer;
        d.item = content.ToObject<FreezeEntity>();
        return d;
    }

    public class FreezeEntity
    {
        public Audio audio;
        public List<string> images;
        public Type type;//类型
        public bool isNext;//是否展示下一环节
        public bool isRepeat;//是否展示从头播放按钮
        public bool isAuto;//是否自动播放
        public bool isLoop;//是否循环播放
        public List<Video> videoList;//视频
        //可能返回空 
        public string pudImage;//布丁形象
        public string pudDynamic;//布丁动效
        public List<AudioAndImage> imgList;

        public enum Type
        {
            audioAndImage,//音频+图片展示
            noDisplay,//不增加额外展示
            audio, //音频
            video,//视频
            audioAndDynamic,//音频+布丁动效
            manyAudioAndImage //多图片+多音频
        }

        public class Audio
        {
            public string content;
            public Type type;

            public enum Type
            {
                tts,
                audio
            }
        }


        public class Video
        {
            public string video;
        }

        public enum PudImage
        {
            magician
        }

        public enum PudDynamic
        {
            commentary
        }

        public class AudioAndImage
        {
            public string audio;
            public string img;
            public bool isPlay;
        }
    }
}
