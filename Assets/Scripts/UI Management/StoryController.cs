using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeEffect : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public float typingSpeed = 0.05f;
    public float fadeDuration = 1f;
    public float visibleDuration = 3f;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private string[] textLines = {
        "Oliver Odd has always been an unusual fellow.",
        "The night before his wedding to his beloved Daisy Dusk was no different.",
        "His nerves had gotten the best of him, and he had a fright about the future.",
        "Not knowing what to do, he ran off into the woods.",
        "It soon became clear to him that he wished for nothing more than to spend the rest of his life with Daisy.",
        "But soon he realized that it was already too late, as he found himself lost in an ancient graveyard, haunted by spirits.",
        "Now he needs your help.",
        "Escape the spirits and collect all the pieces of the graveyard map to guide Oliver out.",
        "Fail to do so, and Oliver will stay trapped there for eternity."
    };

    private void Start()
    {
        uiText.text = "";
        audioSource.clip = audioClip;
        StartCoroutine(DisplayTextWithFade());
    }

    private IEnumerator DisplayTextWithFade()
    {
        foreach (string line in textLines)
        {
            uiText.text = "";
            yield return StartCoroutine(FadeTextToAlpha(0, 0.7f, fadeDuration));
            yield return StartCoroutine(TypeSentence(line));
            yield return new WaitForSeconds(visibleDuration);
            yield return StartCoroutine(FadeTextToAlpha(0.7f, 0, fadeDuration));
        }

        LevelManager.Instance.LoadScene("MainMenu", "CrossFade");
        MusicManager.Instance.PlayMusic("MainMenu");
    }

    private IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            uiText.text += letter;
            audioSource.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator FadeTextToAlpha(float startAlpha, float endAlpha, float duration)
    {
        Color startColor = uiText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            uiText.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiText.color = endColor;
    }
}
