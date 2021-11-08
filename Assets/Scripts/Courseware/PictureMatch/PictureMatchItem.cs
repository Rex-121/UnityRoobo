using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using System;
[RequireComponent(typeof(SpriteRenderer))]
public class PictureMatchItem : MonoBehaviour
{

    [Serializable]
    public enum State
    {
        normal, choosen, right, wrong
    }

    private State _state;


    [SerializeField]
    private Transform upsideDownPoint;

    [ShowInInspector, PropertyOrder(-1), LabelText("当前状态")]
    public State state
    {
        get
        {
            return _state;
        }

        set
        {

            if (value == State.right)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            if (value == State.wrong)
            {
                GetComponent<Collider2D>().enabled = false;

                delayCon = StartCoroutine(UnSelectableWhileWrong());
            }


            if (_state == State.right) return;

            _state = value;
            switch (_state)
            {
                case State.normal:
                    sprite.sprite = file.normal;
                    break;
                case State.choosen:
                    sprite.sprite = file.choosen;
                    break;
                case State.right:
                    sprite.sprite = file.right;
                    break;
                case State.wrong:
                    sprite.sprite = file.wrong;
                    break;

            }
        }
    }

    Coroutine delayCon;

    private void OnDestroy()
    {
        if (delayCon != null)
        {
            StopCoroutine(delayCon);
        }
    }

    [LabelText("KEY")]
    public String key;

    [LabelText("上下翻转")]
    public bool upsideDown = false;


    private SpriteRenderer sprite;


    [SerializeField]
    private PictureMatchItem_SO file;

    [SerializeField]
    private DrawLine line;


    IEnumerator UnSelectableWhileWrong()
    {
        yield return new WaitForSeconds(2);

        line.ClearLine();

        GetComponent<Collider2D>().enabled = true;
        state = State.normal;
    }


    public Action<PictureMatchItem> DidTouchItem;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        state = State.normal;

        sprite.flipY = upsideDown;

        if (upsideDown) { line.transform.position = upsideDownPoint.position; }

    }


    private void OnMouseDown()
    {

        if (state == State.right) return;

        ToggleNormalAndChoosen();

        DidTouchItem?.Invoke(this);

    }

    void ToggleNormalAndChoosen()
    {
        state = state == State.choosen ? State.normal : State.choosen;
    }

    public bool MatchTo(PictureMatchItem item)
    {
        state = key == item.key ? State.right : State.wrong;

        item.state = state;

        line.DrawTheLine(line.transform.position, item.line.transform.position);

        return Matched();
    }


    bool Matched()
    {
        return state == State.right;
    }

}
