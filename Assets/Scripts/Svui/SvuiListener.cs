using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvuiListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DidEndSvui(string message)
    {
        Svui.Instance.SvuiMessageDeliver(message);
    }

}
