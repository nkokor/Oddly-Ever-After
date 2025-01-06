using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public GameObject canvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "key_1")
        {
            StartCoroutine(HandleKey("LevelUp", "Map", "Map", 2, 1));
        }
        else if (other.gameObject.tag == "key_2")
        {
            StartCoroutine(HandleKey("LevelUp", "Map", "Map", 3, 2));
        }
        else if (other.gameObject.tag == "key_3")
        {
            StartCoroutine(HandleKey("LevelUp", "HappyEnding", "Victory", 1, 0));
        }
    }

    private IEnumerator HandleKey(string sound, string sceneName, string music, int unlockedLevel, int completedLevel)
    {
        canvas.SetActive(true);
        SoundManager.Instance.PlaySound2D(sound);

        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
        PlayerPrefs.SetInt("CompletedLevel", completedLevel);
        PlayerPrefs.Save();

        yield return new WaitForSeconds(2.5f);

        LevelManager.Instance.LoadScene(sceneName, "CrossFade");
        MusicManager.Instance.PlayMusic(music);
    }
}

