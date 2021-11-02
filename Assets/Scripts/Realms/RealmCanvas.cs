using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RealmCanvas : MonoBehaviour
{


    [SerializeField]
    private GameObject island;

    [SerializeField]
    private GameObject releam;


    private void Start()
    {
        releam.SetActive(true);
        island.SetActive(false);
    }


    public void MenuToggle()
    {
        releam.SetActive(!releam.activeSelf);
        island.SetActive(!releam.activeSelf);
    }

    public void LoadScene()
    {


        SceneManager.LoadScene("SampleScene");

    }

}

