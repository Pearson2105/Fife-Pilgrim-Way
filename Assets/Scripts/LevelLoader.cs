using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoad;
    public GameObject confirmationPanel; // Drag your UI panel here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the popup and pause the game
            confirmationPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    // Call this from the "Yes" button
    public void ConfirmLoad()
    {
        Time.timeScale = 1f; // Reset time
        SceneManager.LoadScene(levelToLoad);
    }

    // Call this from the "No" button
    public void CancelLoad()
    {
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}