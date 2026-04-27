using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [Header("UI & Transition Settings")]
    public string levelToLoad;
    public GameObject confirmationPanel;
    
    [Header("Transition Components")]
    public CanvasGroup curtainCanvasGroup; // The Black Screen Image
    public CanvasGroup videoCanvasGroup;  // The CanvasGroup on the Video object
    public VideoPlayer transitionVideo;   // The Video Player component

    private bool isPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInRange = false;
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q) && Time.timeScale > 0)
        {
            OpenConfirmation();
        }
    }

    public void OpenConfirmation()
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ConfirmLoad()
    {
        Time.timeScale = 1f;
        confirmationPanel.SetActive(false);
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        // 1. Fade to Black (Curtain)
        yield return Fade(curtainCanvasGroup, 0f, 1f, 1.0f);

        // 2. Play Video
        if (transitionVideo != null)
        {
            // Ensure video object is active and visible
            transitionVideo.gameObject.SetActive(true);
            if (videoCanvasGroup != null) videoCanvasGroup.alpha = 1f;

            transitionVideo.Play();
            
            // Wait for video length
            yield return new WaitForSeconds((float)transitionVideo.clip.length);
            
            // Cleanup
            if (videoCanvasGroup != null) videoCanvasGroup.alpha = 0f;
            transitionVideo.gameObject.SetActive(false);
        }

        // 3. Load next scene
        SceneManager.LoadScene(levelToLoad);
    }

    private IEnumerator Fade(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }

    public void CancelLoad()
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}