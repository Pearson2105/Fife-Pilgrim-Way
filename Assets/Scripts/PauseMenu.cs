using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
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
        SceneManager.LoadScene("0.MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}