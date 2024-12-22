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
            Debug.Log("Key 1 collected! Loading scene...");
            LevelManager.Instance.LoadScene("MidwayMap", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");

        } else if (other.gameObject.tag == "key_2")
        {
            LevelManager.Instance.LoadScene("FinalMap", "CrossFade");
            MusicManager.Instance.PlayMusic("Map");
        } else if (other.gameObject.tag == "key_3")
        {
            LevelManager.Instance.LoadScene("HappyEnding", "CrossFade");
            MusicManager.Instance.PlayMusic("MainMenu");
        }
    }
}

