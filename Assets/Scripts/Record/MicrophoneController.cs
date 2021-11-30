using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MicrophoneController : MonoBehaviour
{
    [Required]
    public Sprite disabledSprite, enabledSprite;
    public float recordDuration = 3f;

    public SpriteRenderer a;

    // Start is called before the first frame update
    void Start()
    {
        a.material.SetFloat("fadfa", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
