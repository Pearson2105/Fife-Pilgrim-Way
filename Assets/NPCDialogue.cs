using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Cards")]
    public DialogueData introCard;  
    public DialogueData priceCard;  
    public DialogueData barterCard; 

    [Header("References")]
    public DialogueManager manager; 
    public GameObject choiceButtons; 
    public GameObject rollButton;    

    private bool isPlayerNearby;
    private bool isDialogueActive = false;

    void Update() {
        if (isPlayerNearby && !isDialogueActive && Input.GetKeyDown(KeyCode.E)) {
            StartDialogue();
        }
    }

    void StartDialogue() {
        isDialogueActive = true;
        manager.dialogueBox.SetActive(true);
        
        // FORCING THE TEXT: This stops the "H" glitch
        manager.dialogueText.text = introCard.dialogueLines[0];

        choiceButtons.SetActive(true);
        rollButton.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlayerSayYes() {
        choiceButtons.SetActive(false);
        manager.dialogueText.text = priceCard.dialogueLines[0];
        Invoke("ShowBarterPrompt", 2.5f);
    }

    void ShowBarterPrompt() {
        manager.dialogueText.text = barterCard.dialogueLines[0];
        rollButton.SetActive(true);
    }

    public void RollDice() {
        rollButton.SetActive(false);
        int roll = Random.Range(1, 21);
        manager.dialogueText.text = (roll >= 15) ? 
            $"You rolled a {roll}! Fine, take the fish." : 
            $"A {roll}? No fish for you!";
        Invoke("EndDialogue", 3f);
    }

    public void OnPlayerSayNo() {
        choiceButtons.SetActive(false);
        manager.dialogueText.text = "Suit yourself!";
        Invoke("EndDialogue", 1.5f);
    }

    public void EndDialogue() {
        isDialogueActive = false;
        manager.dialogueBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) isPlayerNearby = true; }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) { isPlayerNearby = false; EndDialogue(); } }
}
