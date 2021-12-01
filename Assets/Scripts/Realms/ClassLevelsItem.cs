using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ClassLevelsItem : MonoBehaviour
{
    public Text label;

    public string leadingText = "L";

    public int index = 0;

    void Start()
    {
        label.text = leadingText + "" + (index + 1);
    }

}
