using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySkip : MonoBehaviour
{
    public void SkipStory() {
        LevelManager.Instance.LoadScene("MainMenu", "CrossFade");
        MusicManager.Instance.PlayMusic("MainMenu");
    }

}
