using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class Cruiser : MonoBehaviour
{
    [Required]
    public IScrollLimiter scrollLimiter;
    private float end;
    private float start;
    // Start is called before the first frame update
    void Start()
    {
        //正确的值应该是screen width ➗2 但是这里不➗2是为了让飞行物飞出屏幕
         end = GetComponentInParent<PositionBridge>().getRight()+ScreenSize.getScreenWidth();
        start = -ScreenSize.getScreenWidth() / 2;
        Debug.Log("limit:"+end);
        transform.DOMoveX(end,5);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
