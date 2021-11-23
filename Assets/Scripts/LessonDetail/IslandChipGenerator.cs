using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandChipGenerator : MonoBehaviour
{
    [LabelText("标题")]
    [Required]
    public Text text;
    [LabelText("图标")]
    [Required]
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "测试";   
    }

    public void onItemClick() {
        Logging.Log("on item click");
    }
}
