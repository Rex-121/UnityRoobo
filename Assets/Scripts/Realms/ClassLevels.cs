using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;

public class ClassLevels : MonoBehaviour
{

    public struct Level
    {
        public int value;

        public Level(int level)
        {
            value = level;
        }
    }

    [ShowInInspector]
    public Toggle[] toggles;

    public ReactiveProperty<Level> level = new ReactiveProperty<Level>();

    private void OnEnable()
    {
        toggles[0].isOn = true;
    }

    public void OnToggle()
    {

        for (int i = 0; i < toggles.Length; i++)
        {
            var toggle = toggles[i];

            if (toggle.isOn)
            {
                level.Value = new Level(i + 1);
                break;
            }
        }
    }
}
