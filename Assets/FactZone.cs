using UnityEngine;
using TMPro;
using System.Collections;

public class FactZone : MonoBehaviour
{
    [Header("References")]
    public DialogueManager manager;

    [Header("NPC UI (Optional)")]
    public GameObject choiceButtons; 
    public GameObject rollButton;    

    [Header("Content")]
    public string buildingName; // <--- Type the building name here!
    [TextArea(3, 10)]
    public string historicalFact;

    [Header("Scene Transition")]
    public string nextSceneName;
    public string transitionName = "CrossFade";

    [Header("Fade Settings")]
    public float textFadeDuration = 0.5f;

    private bool isPlayerInside = false;
    private bool isTransitioning = false;

    void Update()
    {
        if (manager == null || !isPlayerInside || isTransitioning)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleFact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FadeAndTransition());
        }
    }

    void ToggleFact()
    {
        if (manager == null || manager.dialogueBox == null) return;

        if (!manager.dialogueBox.activeSelf)
        {
            manager.dialogueBox.SetActive(true);
            
            // --- NEW: Set the Name Label to the building name ---
            if (manager.nameLabel != null)
            {
                manager.nameLabel.text = buildingName;
            }

            manager.dialogueText.text = historicalFact;

            SetTextAlpha(1f);

            if (choiceButtons != null) choiceButtons.SetActive(false);
            if (rollButton != null) rollButton.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            manager.dialogueBox.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    IEnumerator FadeAndTransition()
    {
        isTransitioning = true;
        Debug.Log("STARTING FADE + TRANSITION");

        if (manager.dialogueText == null)
        {
            Debug.LogError("Dialogue Text is NULL!");
            yield break;
        }

        float elapsed = 0f;
        Color startColor = manager.dialogueText.color;

        while (elapsed < textFadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsed / textFadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        SetTextAlpha(0f);

        if (manager.dialogueBox != null)
            manager.dialogueBox.SetActive(false);

        if (LevelManager.Instance != null)
        {
            Debug.Log("LOADING SCENE: " + nextSceneName);
            LevelManager.Instance.LoadScene(nextSceneName, transitionName);
        }
        else
        {
            Debug.LogError("LevelManager instance NOT FOUND!");
        }
    }

    void SetTextAlpha(float alpha)
    {
        if (manager.dialogueText != null)
        {
            Color color = manager.dialogueText.color;
            color.a = alpha;
            manager.dialogueText.color = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Player entered FactZone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (manager != null && manager.dialogueBox != null)
                manager.dialogueBox.SetActive(false);
            
            // Re-lock cursor if player walks away while it's open
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Debug.Log("Player exited FactZone");
        }
    }
}