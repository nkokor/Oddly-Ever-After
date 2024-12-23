using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMapManager : MonoBehaviour
{
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    [SerializeField] private Image levelImage; 
    [SerializeField] private Sprite level1Sprite; 
    [SerializeField] private Sprite level2Sprite; 
    [SerializeField] private Sprite level3Sprite; 

    void Start()
    {
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
        }
        if (!PlayerPrefs.HasKey("CompletedLevel"))
        {
            PlayerPrefs.SetInt("CompletedLevel", 0);
        }

        UpdateButtonState(level1Button, 1);
        UpdateButtonState(level2Button, 2);
        UpdateButtonState(level3Button, 3);

        UpdateLevelImage();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene Loaded: {scene.name}, Mode: {mode}");

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); 
        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0);

        Debug.Log($"Unlocked Level: {unlockedLevel}, Completed Level: {completedLevel}");

        UpdateButtonState(level1Button, 1);
        UpdateButtonState(level2Button, 2);
        UpdateButtonState(level3Button, 3);

        UpdateLevelImage();

        Debug.Log($"Level 1 Button - Active: {level1Button.gameObject.activeSelf}, Interactable: {level1Button.interactable}");
        Debug.Log($"Level 2 Button - Active: {level2Button.gameObject.activeSelf}, Interactable: {level2Button.interactable}");
        Debug.Log($"Level 3 Button - Active: {level3Button.gameObject.activeSelf}, Interactable: {level3Button.interactable}");
    }

    private void UpdateButtonState(Button button, int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); 
        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0); 

        if (levelIndex > unlockedLevel)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
            
            if (levelIndex <= completedLevel)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }

    private void UpdateLevelImage()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); 

        switch (unlockedLevel)
        {
            case 1:
                levelImage.sprite = level1Sprite; 
                break;
            case 2:
                levelImage.sprite = level2Sprite;
                break;
            case 3:
                levelImage.sprite = level3Sprite; 
                break;
            default:
                levelImage.sprite = level1Sprite; 
                break;
        }
    }

    public static void MarkLevelAsCompleted(int completedLevel)
    {
        int currentCompleted = PlayerPrefs.GetInt("CompletedLevel", 0);
        if (completedLevel > currentCompleted)
        {
            PlayerPrefs.SetInt("CompletedLevel", completedLevel);
            PlayerPrefs.Save();
        }

        UnlockNextLevel(completedLevel);
    }

    public static void UnlockNextLevel(int completedLevel)
    {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (completedLevel + 1 > currentUnlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", completedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
