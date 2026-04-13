using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData myCard;
    public DialogueManager manager;
    private Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Triggers when you click the NPC's Collider2D
    void OnMouseDown()
    {
        if (manager != null && myCard != null)
        {
            manager.PlayCard(myCard, myAnimator);
        }
    }
}