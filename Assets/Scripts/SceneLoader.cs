using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string SceneName;
    public string Soundtrack;

    public void SwitchScene()
    {
        LevelManager.Instance.LoadScene(SceneName, "CrossFade");
        MusicManager.Instance.PlayMusic(Soundtrack);
    }
}

