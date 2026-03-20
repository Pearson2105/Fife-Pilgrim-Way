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
    public AK.Wwise.Event Play_Scots_Speech;
    public string pitchRtpcName = "Voice_Gender";

    private bool isTyping = false; // The warning was here!

    public void PlayCard(DialogueData card)
    {
        // 1. ADDED THIS: Use the variable to stop double-typing
        if (isTyping) return; 

        if (nameTagText != null) nameTagText.text = card.characterName;

        // 2. Set the Pitch (0 = High, 100 = Deep)
        AkUnitySoundEngine.SetRTPCValue(pitchRtpcName, card.genderPitch);

        dialogueBox.SetActive(true);
        StopAllCoroutines();
        
        if (card.dialogueLines.Length > 0)
        {
            StartCoroutine(TypeLines(card.dialogueLines[0], card.typingSpeed));
        }
    }

    IEnumerator TypeLines(string lineToType, float speed)
    {
        isTyping = true; // Now Unity sees this is used!
        dialogueText.text = "";

        float finalSpeed = speed <= 0 ? 0.05f : speed;

        foreach (char c in lineToType.ToCharArray())
        {
            dialogueText.text += c;

            if (char.IsLetterOrDigit(c))
            {
                AkUnitySoundEngine.SetState("Letters", c.ToString().ToUpper());
                Play_Scots_Speech.Post(gameObject);
            }

            yield return new WaitForSeconds(finalSpeed);
        }

        isTyping = false; 
    }
}