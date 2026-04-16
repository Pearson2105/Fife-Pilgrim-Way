using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    
    [Header("Wwise Global Names")]
    public string beepEventName = "Play_Text_Beep";
    public string pitchRTPC = "GenderPitch";
    public string wobbleRTPC = "VoiceWobble";

    private Queue<string> sentences = new Queue<string>();
    private DialogueData currentData;
    private Animator currentAnimator;
    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool active = false;

    void Update()
    {
        if (!active) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping) cancelTyping = true;
            else DisplayNextSentence();
        }
    }

    public void PlayCard(DialogueData data, Animator npcAnimator)
    {
        active = true;
        currentData = data;
        currentAnimator = npcAnimator;
        
        dialogueBox.SetActive(true);
        if (currentAnimator != null) currentAnimator.SetBool("isTalking", true);

        // Set Wwise parameters
        AkSoundEngine.SetSwitch(currentData.switchGroup, currentData.voiceType, gameObject);
        AkSoundEngine.SetRTPCValue(pitchRTPC, currentData.genderPitch);
        AkSoundEngine.SetRTPCValue(wobbleRTPC, currentData.voiceWobble);

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
            if (cancelTyping) { dialogueText.text = sentence; break; }
            dialogueText.text += letter;
            letterCount++;

            if (letterCount % 2 == 0) AkSoundEngine.PostEvent(beepEventName, gameObject);
            yield return new WaitForSeconds(currentData.typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        active = false;
        if (currentAnimator != null) currentAnimator.SetBool("isTalking", false);
        dialogueBox.SetActive(false);
    }
}
