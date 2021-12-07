using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

public enum CoursewareType
{
    [LabelText("未知")]
    unknow,

    // 拼图
    [LabelText("拼图")]
    puzzle,


    // 电子绘本
    [LabelText("电子绘本")]
    cartoonBooks,



    // 连线
    [LabelText("连线")]
    match,


    // 定格
    [LabelText("定格")]
    freeze,


    // 跟读点读（诗词）
    [LabelText("跟读点读（诗词）")]
    follow,

    // 圈图点读
    [LabelText("圈图点读")]
    circles,


    // 圈图点读
    [LabelText("暂停（Round）")]
    justPause,
}



class CoursewareTypeJSONTranslate : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {

        if (reader.TokenType == JsonToken.String)
        {
            try
            {
                return serializer.Deserialize<CoursewareType>(reader);
            }
            catch (Exception e)
            {
                Logging.Log("发现不支持的题型---->");
                Logging.Log("题型为 ->>! " + reader.Value.ToString() + " !<<--");
                Logging.Log(e.Message);
                Logging.Log("<----发现不支持的题型");
                return CoursewareType.unknow;
            }


        }
        return CoursewareType.unknow;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}