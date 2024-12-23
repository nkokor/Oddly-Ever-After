using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1); 
        PlayerPrefs.SetInt("CompletedLevel", 0);  
        PlayerPrefs.Save(); 

        MusicManager.Instance.PlayMusic("MainMenu");
    }

    void Update()
    {
        
    }
}
