using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for better quality

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    
    [Header("Wwise Config")]
    public string beepEventName = "Play_Text_Beep"; // Your Wwise Event
    public string pitchRTPCName = "GenderPitch";
    public string wobbleRTPCName = "VoiceWobble";

    private Queue<string> sentences = new Queue<string>();
    private DialogueData currentData;
    private Animator currentAnimator;
    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool active = false;

    void Awake()
    {
        if (dialogueBox != null) dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (!active) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping) cancelTyping = true;
            else DisplayNextSentence();
        }
    }

    // This matches the "PlayCard" call in your Cutscene script
    public void PlayCard(DialogueData data, Animator npcAnimator = null)
    {
        active = true;
        currentData = data;
        currentAnimator = npcAnimator;
        
        if (dialogueBox != null) dialogueBox.SetActive(true);
        if (currentAnimator != null) currentAnimator.SetBool("isTalking", true);

        // --- WWISE SETUP ---
        // Set the Switch (e.g., Switch Group "VoiceType", State "Priest")
        AkSoundEngine.SetSwitch(currentData.switchGroup, currentData.voiceType, gameObject);
        
        // Set the RTPCs
        AkSoundEngine.SetRTPCValue(pitchRTPCName, currentData.genderPitch);
        AkSoundEngine.SetRTPCValue(wobbleRTPCName, currentData.voiceWobble);

        sentences.Clear();
        foreach (string s in data.dialogueLines) sentences.Enqueue(s);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) { EndDialogue(); return; }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;
        cancelTyping = false;
        int letterCount = 0;

        foreach (char letter in sentence.ToCharArray())
        {
            if (cancelTyping)
            {
                dialogueText.text = sentence;
                break;
            }

            dialogueText.text += letter;
            letterCount++;

            // BEEP every 2nd letter using Wwise
            if (letterCount % 2 == 0)
            {
                AkSoundEngine.PostEvent(beepEventName, gameObject);
            }

            yield return new WaitForSeconds(currentData.typingSpeed);
        }
        isTyping = false;
        cancelTyping = false;
    }

    void EndDialogue()
    {
        active = false;
        if (currentAnimator != null) currentAnimator.SetBool("isTalking", false);
        if (dialogueBox != null) dialogueBox.SetActive(false);
    }
}