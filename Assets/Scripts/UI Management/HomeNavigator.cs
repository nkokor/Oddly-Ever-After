using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeNavigator : MonoBehaviour
{
    [SerializeField] private GameObject homeDialog;

    public void ShowDialog()
    {
        if (homeDialog != null)
        {
            homeDialog.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Home Dialog GameObject is not assigned in the inspector.");
        }
    }

    public void HideDialog()
    {
        if (homeDialog != null)
        {
            homeDialog.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Home Dialog GameObject is not assigned in the inspector.");
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
