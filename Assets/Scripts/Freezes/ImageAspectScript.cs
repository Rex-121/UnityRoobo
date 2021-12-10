using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ImageAspectScript : MonoBehaviour
{
    [LabelText("重播按钮")]
    public Button mReplay;

    public System.Action<GameObject> onClick;

    public System.Action<GameObject,CW_Freeze_SO.FreezeEntity.AudioAndImage> onClickAudioImage;

    public CW_Freeze_SO.FreezeEntity.AudioAndImage andImage;

    public void OnClickToMoteScale()
    {
        if (onClick != null)
        {
            onClick(this.gameObject);
        }
        

        if (andImage != null && onClickAudioImage!=null)
        {
            onClickAudioImage(this.gameObject, andImage);
        }
       
    }


}