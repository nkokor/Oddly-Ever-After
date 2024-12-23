using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name} (Tag: {other.gameObject.tag})");

        if (other.gameObject.tag == "key_1")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 2); 
            PlayerPrefs.SetInt("CompletedLevel", 1);  
            PlayerPrefs.Save(); 

            Debug.Log("Level 1 completed, unlocking level 2");

            // Ispisivanje trenutnog statusa u PlayerPrefs
            Debug.Log($"Completed Level: {PlayerPrefs.GetInt("CompletedLevel")}, Unlocked Level: {PlayerPrefs.GetInt("UnlockedLevel")}");

            LevelManager.Instance.LoadScene("Map", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");
        } 
        else if (other.gameObject.tag == "key_2")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 3); 
            PlayerPrefs.SetInt("CompletedLevel", 2);  
            PlayerPrefs.Save(); 

            Debug.Log("Level 2 completed, unlocking level 3");

            // Ispisivanje trenutnog statusa u PlayerPrefs
            Debug.Log($"Completed Level: {PlayerPrefs.GetInt("CompletedLevel")}, Unlocked Level: {PlayerPrefs.GetInt("UnlockedLevel")}");

            LevelManager.Instance.LoadScene("Map", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");
        } 
        else if (other.gameObject.tag == "key_3")
        {
            PlayerPrefs.SetInt("UnlockedLevel", 4); 
            PlayerPrefs.SetInt("CompletedLevel", 3);  
            PlayerPrefs.Save(); 

            Debug.Log("Level 3 completed, loading happy ending");

            // Ispisivanje trenutnog statusa u PlayerPrefs
            Debug.Log($"Completed Level: {PlayerPrefs.GetInt("CompletedLevel")}, Unlocked Level: {PlayerPrefs.GetInt("UnlockedLevel")}");

            LevelManager.Instance.LoadScene("HappyEnding", "CrossFade");
            MusicManager.Instance.PlayMusic("MainMenu");
        }
    }
}
