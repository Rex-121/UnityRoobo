using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class DragTarget : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [Required]
    private AudioSource audioSource;
    public SoundRightWrong_SO rwSO;
    private Collider2D currentCollision;
    private DragItem currentCollisionDragItem;
    private PuzzleManager puzzleManager;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        puzzleManager= transform.parent.GetComponent<PuzzleManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != currentCollision)
        {
            currentCollision = collision;
            currentCollisionDragItem = collision.GetComponent<DragItem>();
        }
        Logging.Log("doomed!!!");
        currentCollisionDragItem.setDoomed(true);
        Debug.Log("on collision!!!" + currentCollisionDragItem);
        if (null == currentCollisionDragItem)
        {
            return;
        }
        if (currentCollisionDragItem.isCollidable())
        {

            if (collision.transform.name.Equals("dragItem"+name.Replace("dragTarget","")))
            {
                audioSource.clip = rwSO.right;
                audioSource.Play();
                spriteRenderer.sprite = collision.transform.GetComponent<SpriteRenderer>().sprite;
                transform.localScale = collision.transform.localScale;
                Destroy(collision.gameObject);
                GetComponent<Collider2D>().enabled = false;
                puzzleManager.onePuzzleSolved();
            }
            else
            {
                audioSource.clip = rwSO.wrong;
                audioSource.Play();
                currentCollisionDragItem.setDoomed(false);
                currentCollisionDragItem.moveToOriginalPosition();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (null != currentCollisionDragItem)
        {
            Logging.Log("released!!!!");
            currentCollisionDragItem.setDoomed(false);
        }
    }
}
