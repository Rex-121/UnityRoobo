using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "画板选择题Player", menuName = "课件/Player/画板选择题Player")]
public class CW_PictrueBoardChoice_SO : CoursewarePlayer_SO
{
    public override bool MakeData(GameObject player)
    {
        player.GetComponent<CW_PictureBoardChoice>().InitGridAndData(item);
        return true;
    }

    public PictureBoardEntity item;
    public override CoursewarePlayer_SO ParseData(JToken content)
    {
        Debug.Log("CW_PictrueBoardChoice_SO= " + content);
        var d = CreateInstance<CW_PictrueBoardChoice_SO>();
        d.coursewarePlayer = coursewarePlayer;
        d.item = content.ToObject<PictureBoardEntity>();
        return d;
    }

    public class PictureBoardEntity {

        public string optionType;
        public TemplateType templateType;
        public List<Option> options;
        public Question question;
        public Feedback rightFeedback;
        public Feedback errorFeedback;
        public Feedback subject;
        public QuestionStyle style;


        public enum TemplateType
        {
            voiceChooseImage
        }

        public class Option
        {
            public string audio;//url类型
            public string content;
            public string id;
            public int isAnswer;
            public bool playing;
        }

        public class Question
        {
            public string content;
            public string text;
            public string type;
        }

        public class Feedback
        {
            public string content;
            public string type;
        }


        public enum QuestionStyle
        {
            [LabelText("博物馆-建议人文")]
            museum,
            [LabelText("乐谱-建议音乐")]
            music,
            [LabelText("游乐场-建议英文")]
            playground,
            [LabelText("画板-建议美术")]
            board
             
        }

    }
}
