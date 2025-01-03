using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundControl : MonoBehaviour
{
    public void PlayUISound(string name)
    {
        SoundManager.Instance.PlaySound2D(name);
    }
}
