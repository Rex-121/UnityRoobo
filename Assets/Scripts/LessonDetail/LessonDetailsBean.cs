using System.Collections.Generic;

public class LessonDetailsBean
{
    [System.Serializable]
    public struct ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string endAction;
        /// <summary>
        /// 
        /// </summary>
        public string icon;
        /// <summary>
        /// 
        /// </summary>
        public int id;
        /// <summary>
        /// 
        /// </summary>
        public string intro;
        /// <summary>
        /// 
        /// </summary>
        public int lessonId;
        /// <summary>
        /// 欢迎布丁
        /// </summary>
        public string name;
        /// <summary>
        /// 
        /// </summary>
        public int status;
        /// <summary>
        /// 
        /// </summary>
        public string stopImage;
        /// <summary>
        /// 
        /// </summary>
        public string subtype;
        /// <summary>
        /// 
        /// </summary>
        public string type;
    }

    [System.Serializable]
    public struct Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int lastReportId;
        /// <summary>
        /// 
        /// </summary>
        public int lastRoundId;
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list;
        /// <summary>
        /// 
        /// </summary>
        public int reportId;
        /// <summary>
        /// 
        /// </summary>
        public int total;
    }
}
