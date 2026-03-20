using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueCard", menuName = "Dialogue/Card")]
public class DialogueData : ScriptableObject
{
    public string characterName;

    // This makes it a list again!
    [TextArea(3, 10)]
    public string[] dialogueLines; 

    [Header("Voice Settings")]
    [Range(0, 100)]
    public float genderPitch = 50f;
    public float typingSpeed = 0.05f; 
}