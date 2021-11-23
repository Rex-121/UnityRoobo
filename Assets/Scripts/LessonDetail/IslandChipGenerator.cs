using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class IslandChipGenerator : MonoBehaviour
{
    [LabelText("标题")]
    [Required]
    public Text text;
    [LabelText("图标")]
    [Required]
    public SpriteRenderer sprite;
    private LessonDetailsBean.ListItem data;

    public void onItemClick()
    {
        Logging.Log("on item click:" + data.name);
    }

    public void setData(LessonDetailsBean.ListItem item)
    {
        data = item;
        text.text = item.name;

        SpriteUtil.loadImageToSprite(item.icon,sprite,new Vector2(0.5f,0f));
    }
}
