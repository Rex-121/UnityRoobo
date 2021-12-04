using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ImageAspectScript : MonoBehaviour
{

    private Vector3 localPos ;
    private Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isCenter = false;

    public System.Action<GameObject,Vector3,Vector3> onClick;

    public void OnClickToMoteScale()
    {
        //Debug.Log("" + this.name + " localPos=" + localPos + " worldPos=" + worldPos);
        //if (worldPos.x == 0.0f)
        //{
        //    localPos = this.gameObject.transform.localPosition;
        //    Debug.Log("" + this.name + " localPos=" + localPos);
        //    worldPos = this.gameObject.transform.parent.transform.TransformPoint(localPos);
        //    Debug.Log("" + this.name + " worldPos=" + worldPos);
        //}
        onClick(this.gameObject, localPos, worldPos);

        //Debug.Log("" + this.name + " localPos=" + localPos + " worldPos=" + worldPos);
        //if (worldPos.x == 0.0f)
        //{
        //    localPos = this.gameObject.transform.localPosition;
        //    Debug.Log("" + this.name + " localPos=" + localPos);
        //    worldPos = this.gameObject.transform.parent.transform.TransformPoint(localPos);
        //    Debug.Log("" + this.name + " worldPos=" + worldPos);
        //}
        //if (!isCenter)
        //{
        //    transform.DOMove(new Vector3(640, 400, 0), 1);
        //    transform.DOScale(new Vector3(2, 2, 0), 1);

        //}
        //else
        //{
        //    transform.DOMove(worldPos, 1);
        //    transform.DOScale(new Vector3(1, 1, 0), 1);
        //}
        //isCenter = !isCenter;
    }

}