using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class Cruiser : MonoBehaviour
{
    public bool reverse = false;
    [DetailedInfoBox("是否会伴随上下晃动", "就这样")]
    public bool waggle = false;
    [EnableIf("waggle")]
    public float waggleVaule = 1.5f;
    [EnableIf("waggle")]
    public float waggleDuration = 2;
    [Required]
    public float speed;
    private float end;
    private float start;
    private float duration;
    private Tween flyTween;

    // Start is called before the first frame update
    void Start()
    {
        //正确的值应该是screen width ➗2 但是这里不➗2是为了让飞行物飞出屏幕
        end = GetComponentInParent<PositionBridge>().getRight() + ScreenSize.getScreenWidth();
        start = -ScreenSize.getScreenWidth();
        transform.position = new Vector3(reverse ? end : start, transform.position.y, transform.position.z);
        Debug.Log("end:" + end);
        fly();
        if (waggle)
        {
            floating();
        }
    }
    private void fly()
    {
        if (null != flyTween)
        {
            Debug.Log("fly again!");
            flyTween.Restart();
        }
        else
        {
            flyTween = transform.DOMoveX(reverse ? start : end, end / speed)
                                   .SetAutoKill(false)
                                   .OnComplete(() =>
                                   {
                                       transform.position = new Vector3(reverse ? end : start, transform.position.y, transform.position.z);
                                       Delay.Instance.DelayToCall(3, () =>
                                       {
                                           fly();
                                       });
                                   });
        }
    }

    private void floating()
    {
      transform.DOMoveY(transform.position.y+waggleVaule, waggleDuration).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        if (null != flyTween) { flyTween.Kill(); }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
