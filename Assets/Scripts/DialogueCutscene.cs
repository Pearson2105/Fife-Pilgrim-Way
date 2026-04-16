using System.Collections;
using UnityEngine;

public class DialogueCutscene : MonoBehaviour
{
    public Dialogue introCard;        // Changed from DialogueData to Dialogue
    public DialogueManager manager;    // Drag Dialogue_Manager here
    public Animator priestAnimator;    // Keep this to control animations

    void Start()
    {
        StartCoroutine(RunAutoStart());
    }

    IEnumerator RunAutoStart()
    {
        // Wait 1 second so the game feels natural before he speaks
        yield return new WaitForSeconds(1.0f);
        
        if (manager != null && introCard != null)
        {
            // We changed "PlayCard" to "StartCutsceneDialogue"
            // We pass the animator so the manager can trigger talk animations
            manager.StartCutsceneDialogue(introCard, priestAnimator);
        }
    }
}