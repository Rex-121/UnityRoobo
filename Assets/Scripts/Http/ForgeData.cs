using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Newtonsoft.Json.Converters;

public class ForgeData
{



    public struct PictureRound
    {

        public Process process;

        public string src;

    }

    public struct VideoRound
    {

        public List<StopPoints> stopPoints;

        public string src;

    }

    public struct StopPoints
    {
        public int at;
        public Process process;
    }

    public struct Process
    {
        public string type;

        public JToken content { get; set; }

    }

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

        public enum Type
        {
            Video, Picture, PicBook, WordFuns, Dubbing
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }

        //[JsonProperty(propertyName: "content")]
        public JToken content;

        public enum DisplayMode
        {
            [Description("串场")]
            only_play,
            [Description("不展示")]
            no_display,
            [Description("展示")]
            common
        }

        public enum Pipeline
        {
            [Description("返回")]
            pop,
            [Description("暂停")]
            pause,
            [Description("继续")]
            continues
        }


        [JsonConverter(typeof(ConvertEndAction))]
        public Pipeline endAction;

        [JsonProperty(propertyName: "subtype")]
        public DisplayMode displayMode;
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