using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelManager : MonoBehaviour
{
    public void Play()
    {
        LevelManager.Instance.LoadScene("Game", "CrossFade");
        MusicManager.Instance.PlayMusic("Graveyard");
    }
}
