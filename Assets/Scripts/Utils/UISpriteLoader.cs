using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteLoader : MonoBehaviour
{
    [Required]
    public string url;
    [LabelText("图片尺寸类型")]
    public LoadImageSizeType loadImageSizeType = LoadImageSizeType.GameObjectSize;
    [EnableIf("@this.loadImageSizeType==LoadImageSizeType.Fixed||this.loadImageSizeType==LoadImageSizeType.AdapteToWidth")]
    public float width;
    [EnableIf("@this.loadImageSizeType==LoadImageSizeType.Fixed||this.loadImageSizeType==LoadImageSizeType.AdapteToHeight")]
    public float height;
    [LabelText("锚点")]
    public Vector2 anchor = new Vector2(0.5f, 0.5f);
    public float animDuration = 0.3f;

    void Start()
    {
        Image image = GetComponent<Image>();
        SpriteUtil.loadImageToUIImage(url, image, loadImageSizeType, width, height, anchor, animDuration, null);
    }
}
