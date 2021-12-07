using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpriteLoader : MonoBehaviour
{
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
        if (url != null && url != "") {
            loadImage();
        }
    }

    public void setURL(string url) {
        this.url = url;
        loadImage();
    }

    private void loadImage() {
        Image image = GetComponent<Image>();
        SpriteUtil.loadImageToUIImage(url, image, loadImageSizeType, width, height, anchor, animDuration, null);
    }
}
