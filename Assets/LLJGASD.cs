using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LLJGASD : MonoBehaviour
{
    //;
    public int index = 0;

    public Sprite[] spites;

    public Image r;



    public void NEXT()
    {
        index = Mathf.Min(12, index += 1);
        r.sprite = spites[index];
    }


    public void PRE()
    {
        index = Mathf.Max(0, index -= 1);
        r.sprite = spites[index];
    }
}
