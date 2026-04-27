using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;

    [Header("Transition")]
    public string mainMenuSceneName = "0.MainMenu";
    public string transitionName = "CrossFade";

    [Header("Camera")]
    public PlayerMovement cameraController;   // Changed to specific type

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (cameraController != null)
            cameraController.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        settingsPanel.SetActive(false);
        pauseMenuUI.transform.SetAsLastSibling();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (cameraController != null)
            cameraController.enabled = false;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadScene(mainMenuSceneName, transitionName);
        }
        else
        {
            Debug.LogWarning("LevelManager not found, loading scene directly.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}