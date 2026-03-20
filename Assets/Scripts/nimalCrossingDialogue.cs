using System.Collections;
using UnityEngine;
using TMPro;

public class AnimalCrossingDialogue : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameTagText;
    public GameObject dialogueBox;

    [Header("Wwise Settings")]
    public AK.Wwise.Event playLetterSound; 
    public string pitchRtpcName = "GenderPitch";

    private bool isTyping = false;
    private DialogueData currentCard;

    public void PlayCard(DialogueData card)
    {
        // Prevent overlapping if already typing
        if (isTyping) return; 

        currentCard = card;
        if (nameTagText != null) nameTagText.text = card.characterName;

        // 1. Tell Wwise how deep the voice should be
        AkUnitySoundEngine.SetRTPCValue(pitchRtpcName, card.genderPitch);

        dialogueBox.SetActive(true);
        StopAllCoroutines();
        
        if (card.dialogueLines.Length > 0)
        {
            // Start with the first line in the array
            StartCoroutine(TypeLines(card.dialogueLines[0]));
        }
    }

    IEnumerator TypeLines(string lineToType)
    {
        isTyping = true;
        dialogueText.text = "";

        // 2. SPEED COMPENSATION
        // We subtract a tiny bit of time based on how deep the pitch is.
        // Higher pitch value (100) = Lower wait time = Faster typing.
        float baseSpeed = currentCard.typingSpeed <= 0 ? 0.05f : currentCard.typingSpeed;
        float speedOffset = currentCard.genderPitch * 0.0001f; 
        float finalSpeed = Mathf.Max(0.01f, baseSpeed - speedOffset);

        foreach (char c in lineToType.ToCharArray())
        {
            dialogueText.text += c;

            // 3. SOUND TRIGGER
            if (char.IsLetterOrDigit(c))
            {
                string letterState = c.ToString().ToUpper();
                
                // Set the specific letter state (A, B, C...)
                AkUnitySoundEngine.SetSwitch("Letters", letterState, gameObject);
                
                // Play the "Play_Scots_Speech" event
                playLetterSound.Post(gameObject);
            }

            yield return new WaitForSeconds(finalSpeed);
        }

        isTyping = false; 
    }
}