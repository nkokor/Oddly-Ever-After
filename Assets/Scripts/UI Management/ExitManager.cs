using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject Dialog;

    public void OpenDialog()
    {
        if (Dialog != null)
        {
            Dialog.SetActive(true);
        }
    }

    public void CloseDialog()
    {
        if (Dialog != null)
        {
            Dialog.SetActive(false);    
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
