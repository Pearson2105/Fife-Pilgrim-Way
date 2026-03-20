using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueCard", menuName = "Dialogue/Card")]
public class DialogueData : ScriptableObject
{
    public string characterName;

    [TextArea(3, 10)]
    public string[] dialogueLines; 

    [Header("Voice Settings")]
    [Range(0, 100)]
    public float genderPitch = 50f; // 0 = High, 100 = Deep
    public float typingSpeed = 0.05f; 
}