using System.Collections;
using UnityEngine;

public class DialogueCutscene : MonoBehaviour
{
    public DialogueData introCard;
    public DialogueManager manager;
    public Animator priestAnimator;

    void Start()
    {
        StartCoroutine(RunAutoStart());
    }

    IEnumerator RunAutoStart()
    {
        yield return new WaitForSeconds(1.0f);
        if (manager != null && introCard != null)
        {
            manager.PlayCard(introCard, priestAnimator);
        }
    }
}