using System.Collections;
using UnityEngine;

public class DialogueCutscene : MonoBehaviour
{
    public DialogueData introCard;      // Drag Priest_Card here
    public DialogueManager manager;    // Drag Dialogue_Manager here
    public Animator priestAnimator;    // Drag the Priest Sprite here

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
            manager.PlayCard(introCard, priestAnimator);
        }
    }
}