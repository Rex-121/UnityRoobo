using UnityEngine;
using DG.Tweening;

public class DragItem : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool collidable = true;
    private bool doomed=false;//是否正处于交互点中
    private bool tweening = false;
    
    private void OnMouseDown()
    {
        if (tweening) { return; }
        collidable = false;
        if (originalPosition == Vector3.zero)
        {
            Logging.Log("position:" + transform.position);
            originalPosition = transform.position;
        }
    }

    private void OnMouseUp()
    {
        collidable = true;
        if (!doomed)
        {
            moveToOriginalPosition(); 
        }
    }

    public void setDoomed(bool isDoomed) {
        this.doomed = isDoomed;
    }

    public void moveToOriginalPosition() {
        tweening = true;
        transform.DOMove(originalPosition, 0.5f).OnPlay(() => {
            collidable = false;
        }).OnComplete(() => {
            tweening = false;
            collidable = true;
        });
    }

    public bool isCollidable()
    {
        return collidable;
    }
}
