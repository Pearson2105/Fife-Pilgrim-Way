using UnityEngine;
using TMPro;

public class FactZone : MonoBehaviour
{
    [Header("References")]
    public DialogueManager manager; 
    
    [Header("NPC UI (To Hide)")]
    public GameObject choiceButtons;
    public GameObject rollButton;

    [Header("Content")]
    [TextArea(3, 10)]
    public string historicalFact;

    private bool isPlayerInside = false;

    void Update()
    {
        // Added a check: only run if manager is actually assigned
        if (manager != null && isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleFact();
        }
    }

    void ToggleFact()
    {
        if (manager == null || manager.dialogueBox == null) return;

        if (!manager.dialogueBox.activeSelf)
        {
            manager.dialogueBox.SetActive(true);
            manager.dialogueText.text = historicalFact;
            
            if(choiceButtons != null) choiceButtons.SetActive(false);
            if(rollButton != null) rollButton.SetActive(false);
            
            // Unlock mouse for the fact
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if(manager != null && manager.dialogueBox != null)
                manager.dialogueBox.SetActive(false);
        }
    }
}