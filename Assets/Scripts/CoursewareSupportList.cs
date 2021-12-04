using System.Collections.Generic;
using Sirenix.OdinInspector;

public class CoursewareSupportList : SerializedMonoBehaviour
{

    [LabelText("支持的课件列表")]
    [DictionaryDrawerSettings(KeyLabel = "课件", ValueLabel = "SO文件")]
    public Dictionary<CoursewareType, CoursewarePlayer_SO> supports;



}
