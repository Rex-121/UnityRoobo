using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class VideoAspectScript : MonoBehaviour
{

    public System.Action<GameObject> onClick;

    [LabelText("重播按钮")]
    public Button mReplayButton;


    public void OnClickToMoteScale()
    {
        onClick(this.gameObject);
    }

}
