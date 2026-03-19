using UnityEngine;

public class DialogueSceneManager : MonoBehaviour
{
    public AnimalCrossingDialogue dialogueSystem; // Drag your DialogueManager here
    public DialogueData cutsceneDialogue;        // Drag your Priest_Intro card here

    void Start()
    {
        // Start the priest talking the moment the scene begins
        if (dialogueSystem != null && cutsceneDialogue != null)
        {
            dialogueSystem.PlayCard(cutsceneDialogue);
        }
    }
}