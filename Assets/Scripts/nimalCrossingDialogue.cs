using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem; // Using the New Input System

public class AnimalCrossingDialogue : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI uiText;      
    public TextMeshProUGUI nameTagText; // Drag your Name Text here
    public GameObject dialogueBox;     
    public float typingSpeed = 0.05f;  

    [Header("Wwise Settings")]
    public string switchGroupName = "Letters"; 
    public AK.Wwise.Event playLetterSound;    

    [Header("Priest Animation")]
    public Animator priestAnimator;           
    public string talkingBoolName = "isTalking"; 

    public void PlayCard(DialogueData card)
    {
        // Set Character Name from the Card
        if (nameTagText != null) nameTagText.text = card.characterName;

        dialogueBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeLines(card));
    }

    IEnumerator TypeLines(DialogueData card)
    {
        foreach (string line in card.lines)
        {
            uiText.text = "";
            
            if (priestAnimator != null)
                priestAnimator.SetBool(talkingBoolName, true);

            foreach (char letter in line.ToCharArray())
            {
                uiText.text += letter;

                if (char.IsLetterOrDigit(letter))
                {
                    // Convert to Uppercase to match Wwise Switch names
                    string upperLetter = letter.ToString().ToUpper();
                    AkUnitySoundEngine.SetSwitch(switchGroupName, upperLetter, gameObject);
                    playLetterSound.Post(gameObject);
                }

                // Small pause logic for punctuation (Animal Crossing Style)
                if (letter == ',' || letter == '.' || letter == '!')
                    yield return new WaitForSeconds(typingSpeed * 3);
                else
                    yield return new WaitForSeconds(typingSpeed);
            }

            if (priestAnimator != null)
                priestAnimator.SetBool(talkingBoolName, false);

            // WAIT FOR INPUT (New Input System)
            yield return new WaitUntil(() => 
                Mouse.current.leftButton.wasPressedThisFrame || 
                Keyboard.current.spaceKey.wasPressedThisFrame);
            
            yield return new WaitForSeconds(0.1f);
        }

        dialogueBox.SetActive(false);
    }
}