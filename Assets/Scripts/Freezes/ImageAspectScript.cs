using UnityEngine;

public class ImageAspectScript : MonoBehaviour
{
    public System.Action<GameObject> onClick;
    public void OnClickToMoteScale()
    {
        onClick(this.gameObject);

    }

}