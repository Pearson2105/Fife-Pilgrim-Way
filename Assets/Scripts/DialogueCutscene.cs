using System.Collections;
using UnityEngine;

public class DialogueCutscene : MonoBehaviour
{
    public DialogueData introCard;
    public DialogueManager manager;
    public Animator priestAnimator;

    [Header("Transition")]
    public string nextSceneName;
    public string transitionName = "CrossFade";

    void Start()
    {
        StartCoroutine(RunAutoStart());
    }

    IEnumerator RunAutoStart()
    {
        yield return new WaitForSeconds(1.0f);

        if (manager != null && introCard != null)
        {
            manager.OnDialogueFinished += HandleDialogueFinished;
            manager.PlayCard(introCard, priestAnimator);
        }
    }

    void HandleDialogueFinished()
    {
        StartCoroutine(DelayedTransition());
    }

    IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(0.5f);

        LevelManager.Instance.LoadScene(nextSceneName, transitionName);
    }

    void OnDestroy()
    {
        if (manager != null)
            manager.OnDialogueFinished -= HandleDialogueFinished;
    }
}