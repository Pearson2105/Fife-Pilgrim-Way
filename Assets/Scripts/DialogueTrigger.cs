using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // CHANGE THIS LINE: from DialogueData to Dialogue
    public Dialogue dialogue; 
    
    public DialogueManager manager;
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        if (manager == null) manager = FindFirstObjectByType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        if (manager != null)
        {
            // Now this will work because 'dialogue' is the correct type
            manager.PlayCard(dialogue, myAnimator);
        }
    }
}