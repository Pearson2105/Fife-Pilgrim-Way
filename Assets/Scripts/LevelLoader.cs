using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Settings")]
    public string levelToLoad; // Name of the scene
    public GameObject confirmationPanel; // Drag your UI Panel here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenConfirmation();
        }
    }

    public void OpenConfirmation()
    {
        // 1. Show the UI
        if (confirmationPanel != null) confirmationPanel.SetActive(true);

        // 2. Unlock the mouse and show it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 3. Freeze the game
        Time.timeScale = 0f;
    }

    public void ConfirmLoad()
    {
        // Reset time before loading to prevent issues
        Time.timeScale = 1f;
        
        // Load the scene
        SceneManager.LoadScene(levelToLoad);
    }

    public void CancelLoad()
    {
        // 1. Hide the UI
        if (confirmationPanel != null) confirmationPanel.SetActive(false);

        // 2. Relock the mouse (assuming your player script handles the locking)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 3. Resume the game
        Time.timeScale = 1f;
    }
}