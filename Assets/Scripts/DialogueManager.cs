using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; 

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject continueIcon; 

    [Header("Wwise Events")]
    public AK.Wwise.Event playLetterEvent;

    private bool isTyping = false;
    private Animator activeAnimator;

    // This is the function the Player script looks for!
    public bool IsTalking()
    {
        return isTyping;
    }

    public void PlayCard(DialogueData card, Animator npcAnim)
    {
        if (!isTyping)
        {
            activeAnimator = npcAnim;
            StartCoroutine(TypeLines(card));
        }
    }

    IEnumerator TypeLines(DialogueData card)
    {
        isTyping = true;
        
        if (nameText != null) nameText.text = card.characterName;

        // Wwise Audio Setup
        AkUnitySoundEngine.SetSwitch("Voice_Types", card.voiceType, gameObject);
        AkUnitySoundEngine.SetRTPCValue("Voice_Gender", card.genderPitch);
        

        foreach (string line in card.dialogueLines)
        {
            dialogueText.text = ""; 
            if (continueIcon != null) continueIcon.SetActive(false); 
            
            if (activeAnimator != null) activeAnimator.SetBool("isTalking", true);

            int letterCount = 0;
            foreach (char c in line.ToCharArray())
            {
                dialogueText.text += c;

                if (char.IsLetterOrDigit(c))
                {
                    if (letterCount % 2 == 0) playLetterEvent.Post(gameObject);
                    letterCount++;
                }
                yield return new WaitForSeconds(card.typingSpeed);
            }

            if (activeAnimator != null) activeAnimator.SetBool("isTalking", false);
            if (continueIcon != null) continueIcon.SetActive(true); 
            
            // Wait for Space Bar
            yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            yield return new WaitForSeconds(0.1f);
        }

        dialogueText.text = "";
        if (nameText != null) nameText.text = "";
        if (continueIcon != null) continueIcon.SetActive(false);
        isTyping = false;
        activeAnimator = null;
    }
}