using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMapManager : MonoBehaviour
{
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    // Dodajte Image komponentu za prikaz slika zavisno od otključanog nivoa
    [SerializeField] private Image levelImage; // Povežite ovu Image sa scenom u Unity-ju
    [SerializeField] private Sprite level1Sprite; // Dodajte sliku za prvi nivo
    [SerializeField] private Sprite level2Sprite; // Dodajte sliku za drugi nivo
    [SerializeField] private Sprite level3Sprite; // Dodajte sliku za treći nivo

    void Start()
    {
        // Proveravamo da li su podaci već postavljeni u PlayerPrefs
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            // Ako nije postavljen "UnlockedLevel", postavljamo početnu vrednost
            PlayerPrefs.SetInt("UnlockedLevel", 1);
        }
        if (!PlayerPrefs.HasKey("CompletedLevel"))
        {
            // Ako nije postavljen "CompletedLevel", postavljamo početnu vrednost
            PlayerPrefs.SetInt("CompletedLevel", 0);
        }

        // Osiguravamo da se dugmadi ažuriraju odmah na početku igre
        UpdateButtonState(level1Button, 1);
        UpdateButtonState(level2Button, 2);
        UpdateButtonState(level3Button, 3);

        // Ažuriraj sliku na osnovu otključanog nivoa
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

        // Učitavamo podatke o statusu nivoa iz PlayerPrefs
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Otključani nivo
        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0); // Završeni nivo

        // Ispisivanje statusa nivoa
        Debug.Log($"Unlocked Level: {unlockedLevel}, Completed Level: {completedLevel}");

        // Ažuriranje stanja dugmadi nakon učitavanja scene
        UpdateButtonState(level1Button, 1);
        UpdateButtonState(level2Button, 2);
        UpdateButtonState(level3Button, 3);

        // Ažuriraj sliku na osnovu otključanog nivoa
        UpdateLevelImage();

        // Ispisivanje statusa dugmadi nakon što su ažurirane
        Debug.Log($"Level 1 Button - Active: {level1Button.gameObject.activeSelf}, Interactable: {level1Button.interactable}");
        Debug.Log($"Level 2 Button - Active: {level2Button.gameObject.activeSelf}, Interactable: {level2Button.interactable}");
        Debug.Log($"Level 3 Button - Active: {level3Button.gameObject.activeSelf}, Interactable: {level3Button.interactable}");
    }

    private void UpdateButtonState(Button button, int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Otključani nivo
        int completedLevel = PlayerPrefs.GetInt("CompletedLevel", 0); // Završeni nivo

        // Proveravamo koji nivo treba da bude prikazan i kako
        if (levelIndex > unlockedLevel)
        {
            // Dugme je sakriveno ako nivo nije otključan
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
            
            if (levelIndex <= completedLevel)
            {
                // Dugme je onemogućeno ako je nivo završen
                button.interactable = false;
            }
            else
            {
                // Dugme je aktivno ako nivo nije završen, ali je otključan
                button.interactable = true;
            }
        }
    }

    private void UpdateLevelImage()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Otključani nivo

        // Menjanje slike na osnovu otključanog nivoa
        switch (unlockedLevel)
        {
            case 1:
                levelImage.sprite = level1Sprite; // Postavite sliku za prvi nivo
                break;
            case 2:
                levelImage.sprite = level2Sprite; // Postavite sliku za drugi nivo
                break;
            case 3:
                levelImage.sprite = level3Sprite; // Postavite sliku za treći nivo
                break;
            default:
                levelImage.sprite = level1Sprite; // Podrazumevano je slika za prvi nivo
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
