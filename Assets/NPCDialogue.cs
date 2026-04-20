using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Data Card")]
    public DialogueData fishDialogue; // This is your ScriptableObject

    [Header("UI Objects")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcText;
    public GameObject choiceButtons; 
    public GameObject rollButton;    

    private bool isPlayerNearby;
    private bool isDialogueActive = false;

    void Start() {
        dialoguePanel.SetActive(false);
    }

    void Update() {
        // Start talking when pressing E
        if (isPlayerNearby && !isDialogueActive && Input.GetKeyDown(KeyCode.E)) {
            StartDialogue();
        }
    }

    void StartDialogue() {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        
        // Show the first line from our card
        npcText.text = fishDialogue.dialogueLines[0];
        
        choiceButtons.SetActive(true);
        rollButton.SetActive(false);

        // Unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlayerSayYes() {
        choiceButtons.SetActive(false);
        // Show the second line
        npcText.text = fishDialogue.dialogueLines[1];
        Invoke("ShowRollOption", 2f);
    }

    void ShowRollOption() {
        // Show the third line
        npcText.text = fishDialogue.dialogueLines[2];
        rollButton.SetActive(true);
    }

    public void RollDice() {
        rollButton.SetActive(false);
        int roll = Random.Range(1, 21); // Random number between 1 and 20
        
        if (roll >= 15) {
            npcText.text = "You rolled a " + roll + "! Fine, take the fish.";
        } else {
            npcText.text = "A " + roll + "? No fish for you!";
        }
        Invoke("EndDialogue", 3f);
    }

    public void OnPlayerSayNo() {
        choiceButtons.SetActive(false);
        npcText.text = "Suit yourself!";
        Invoke("EndDialogue", 1.5f);
    }

    public void EndDialogue() {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        // Lock mouse back to game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) isPlayerNearby = true; }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) { isPlayerNearby = false; EndDialogue(); } }
}