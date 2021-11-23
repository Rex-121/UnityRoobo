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

        WebReqeust.GetTexture(item.icon,
        (result) =>
        {
            Logging.Log("sprite width" + sprite.sprite.bounds.size.x + " ,result width:" + result.width);
            float originalWidth = sprite.sprite.bounds.size.x * 100f;
            float originalHeight = sprite.sprite.bounds.size.y * 100f;
            //替换sprite 
            sprite.sprite = Sprite.Create(result, new Rect(Vector2.zero, new Vector2(result.width, result.height)), new Vector2(0.5f, 0f));
            //修改大小
            sprite.transform.localScale = new Vector3(originalWidth / result.width, originalHeight/ result.height, sprite.transform.localScale.z);
        }, (msg) =>
        {
            Debug.Log("load image failed:" + msg);
        });
    }
}
