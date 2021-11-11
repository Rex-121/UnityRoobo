using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isCollided;
    [Required]
    private AudioSource audioSource;

    public SoundRightWrong_SO rwSO;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollided)
        {
            return;
        }
        isCollided = true;
        Debug.Log("on collision!!!");
        if (collision.transform.name.Equals("dragItem1"))
        {
            audioSource.clip = rwSO.right;
            audioSource.Play();
            spriteRenderer.sprite = collision.transform.GetComponent<SpriteRenderer>().sprite;
            transform.localScale = collision.transform.localScale;
            Destroy(collision.gameObject);
        }
        else
        {
            audioSource.clip = rwSO.wrong;
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
    }

}
