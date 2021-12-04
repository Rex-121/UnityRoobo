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

    public System.Action<GameObject> onClick;

    public void OnClickToMoteScale()
    {
        onClick(this.gameObject);

    }

}