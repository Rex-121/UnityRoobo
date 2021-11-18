using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Michsky.UI.ModernUIPack;


public class ModalView : MonoBehaviour
{

    public ModalWindowManager modalWindow;

    public void ShowModal()
    {

        modalWindow.OpenWindow();

    }


    public void LOOOOO()
    {

        Logging.Log("Fadsdgasd");

    }
}
