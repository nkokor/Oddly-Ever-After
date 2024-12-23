using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "key_1")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 2); 
            PlayerPrefs.SetInt("CompletedLevel", 1);  
            PlayerPrefs.Save(); 

            LevelManager.Instance.LoadScene("Map", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");
        } 
        else if (other.gameObject.tag == "key_2")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 3); 
            PlayerPrefs.SetInt("CompletedLevel", 2);  
            PlayerPrefs.Save(); 

            LevelManager.Instance.LoadScene("Map", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");
        } 
        else if (other.gameObject.tag == "key_3")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 4); 
            PlayerPrefs.SetInt("CompletedLevel", 3);  
            PlayerPrefs.Save(); 

            LevelManager.Instance.LoadScene("HappyEnding", "CrossFade");
            MusicManager.Instance.PlayMusic("MainMenu");
        }
    }
}
