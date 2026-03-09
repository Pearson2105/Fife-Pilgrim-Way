using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;               

    [Header("Panels & Objects")]
    public GameObject settingsPopUp;
    public FadeManager fadeManager;

    private void Start()
    {
        continueButton.gameObject.SetActive(false);

        if (SaveManager.Instance != null && SaveManager.Instance.SaveExists())
        {
            continueButton.gameObject.SetActive(true);
        }

        newGameButton.onClick.AddListener(OnNewGameClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(OnQuitClicked);
        backButton.onClick.AddListener(CloseSettings);
    }

    private void OnNewGameClicked()
    {
        var newSave = new SaveData { currentLevel = 1 };
        SaveManager.Instance.SaveGame(newSave);
        fadeManager.FadeToBlack("GameScene1");
    }

    private void OnContinueClicked()
    {
        fadeManager.FadeToBlack("GameScene1");
    }

    private void OpenSettings()
    {
        settingsPopUp.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsPopUp.SetActive(false);
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}