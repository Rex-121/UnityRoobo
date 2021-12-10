using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Newtonsoft.Json.Converters;

using Sirenix.OdinInspector;

public class ForgeData
{


    ///pudding/teacher/v1/course/5509/lesson/6979/round/list
    public struct RoundList
    {
        public List<Rounds> list;

        public int total;
    }


    public struct Rounds : RooboEveryInfo
    {
        public int id { get; set; }

        public string name { get; set; }

        public string intro { get; set; }

        public string icon { get; set; }

        public string stopImage { get; set; }

        public enum Type
        {
            [LabelText("视频")]
            Video,
            [LabelText("图片")]
            Picture,
            [LabelText("电子绘本")]
            PicBook,
            [LabelText("闪卡")]
            WordFuns,
            [LabelText("趣配音")]
            Dubbing
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }

        //[JsonProperty(propertyName: "content")]
        public JToken content;

        public enum DisplayMode
        {
            [Description("串场"), LabelText("串场")]
            only_play,
            [Description("不展示"), LabelText("不展示")]
            no_display,
            [Description("展示"), LabelText("展示")]
            common
        }

        public enum Pipeline
        {
            [Description("返回"), LabelText("返回")]
            pop,
            [Description("暂停"), LabelText("暂停")]
            pause,
            [Description("继续"), LabelText("继续")]
            continues
        }


        [JsonConverter(typeof(ConvertEndAction))]
        public Pipeline endAction;

        [JsonProperty(propertyName: "subtype")]
        public DisplayMode displayMode;


        public List<RoundProcess> processList
        {
            get
            {

                var list = new List<RoundProcess>();

                if (content == null) goto exit;

                switch (type)
                {
                    case Type.Picture:

                        Logging.Log(content);

                        list.AddRange(content.ToObject<List<RoundProcess>>());


                        foreach (var l in list)
                        {
                            Logging.Log(l.process.type);
                        }

                        break;
                    case Type.PicBook:
                        Logging.Log(content);
                        list.Add(new RoundProcess("", new RoundProcess.Process(CoursewareType.cartoonBooks, content), 0));
                        break;
                }

            exit:
                return list;
            }
        }

        public VideoRoundProcess videoProcess
        {
            get
            {
                try
                {

                    return content.ToObject<VideoRoundProcess>();
                }

                catch (Exception e)
                {
                    Logging.Log(e.Message);
                }
                return new VideoRoundProcess();
            }
        }
    }


    // 原始的RoundProcess数据
    public struct RoundProcess
    {
        public string src;

        public int at;

        public Process process;

        public struct Process
        {
            [JsonConverter(typeof(CoursewareTypeJSONTranslate))]
            public CoursewareType type;

            public JToken content;

            public Process(CoursewareType type, JToken content)
            {
                this.type = type;
                this.content = content;
            }

        }

        public RoundProcess(string s, Process p, int at)
        {
            src = s;
            this.at = at;
            process = p;
        }
    }

    public struct VideoRoundProcess
    {
        public List<StopPoints> stopPoints;

        public string video;

        public struct StopPoints
        {
            public int at;
            public RoundProcess.Process process;
        }
    }






    public struct Course
    {
        [JsonProperty(propertyName: "courseInfo")]
        public CourseInfo course;


        [JsonProperty(propertyName: "lessonInfo")]
        public CourseInfo lesson;


        public List<Rounds> rounds;
    }


    public interface RooboEveryInfo
    {
        int id { get; set; }
        string name { get; set; }
        string icon { get; set; }
        string intro { get; set; }
    }


    public struct CourseInfo : RooboEveryInfo
    {
        public int id { get; set; }

        public string name { get; set; }

        public string intro { get; set; }

        public string icon { get; set; }
    }
}


class ConvertEndAction : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {

        if (reader.TokenType == JsonToken.String)
        {
            string value = reader.Value.ToString();

            switch (value)
            {
                case "skip":
                    return ForgeData.Rounds.Pipeline.pop;
                case "common":
                    return ForgeData.Rounds.Pipeline.continues;
                case "stop":
                    return ForgeData.Rounds.Pipeline.pause;
            }

        }
        return ForgeData.Rounds.Pipeline.continues;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}






public static class VideoRoundProcessExtension
{

    public static CW_OriginContent cw_OriginContent(this ForgeData.VideoRoundProcess.StopPoints o)
    {
        return new CW_OriginContent(o.process.type, o.process.content, new CW_OriginContent.Joint(o.at));
    }


    public static CW_OriginContent cw_OriginContent(this ForgeData.RoundProcess o)
    {
        return o.process.cw_OriginContent();
    }


    public static CW_OriginContent cw_OriginContent(this ForgeData.RoundProcess.Process o)
    {
        return new CW_OriginContent(o.type, o.content, CW_OriginContent.Joint.Empty());
    }

}