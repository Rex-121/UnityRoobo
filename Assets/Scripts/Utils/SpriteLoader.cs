using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLoader : MonoBehaviour
{
    [Required]
    public string url;
    [LabelText("图片尺寸类型")]
    public LoadImageSizeType loadImageSizeType=LoadImageSizeType.GameObjectSize;
    [EnableIf("@this.loadImageSizeType==LoadImageSizeType.Fixed||this.loadImageSizeType==LoadImageSizeType.AdapteToWidth")]
    public float width;
    [EnableIf("@this.loadImageSizeType==LoadImageSizeType.Fixed||this.loadImageSizeType==LoadImageSizeType.AdapteToHeight")]
    public float height;
    [LabelText("锚点")]
    public Vector2 anchor = new Vector2(0.5f, 0.5f);
    public float animDuration = 0.3f;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteUtil.loadImageToSprite(url,spriteRenderer,loadImageSizeType,width,height,anchor,animDuration,null);
    }
}
