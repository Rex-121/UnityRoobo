using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class MicrophoneController : MonoBehaviour
{
    [Required]
    public Sprite disabledSprite, enabledSprite;
    public float recordDuration = 3f;
    private SpriteRenderer spriteRenderer;
    [Required]
    public SpriteRenderer progressRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DOTween.To(()=> progressRenderer.material.GetFloat("_progress"),x=> progressRenderer.material.SetFloat("_progress",x),1,5 ).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
