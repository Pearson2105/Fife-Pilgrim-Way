using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueCard", menuName = "Dialogue/Card")]
public class DialogueData : ScriptableObject
{
    [Header("Text Content")]
    public string characterName;
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Voice Settings")]
    public string voiceType;       // Must match your Switch name in Wwise (e.g., "Priest")
    [Range(-2400, 2400)] 
    public float genderPitch;      // The base pitch for the voice
    [Range(0, 100)]
    public float voiceWobble;      // The "shakiness" (linked to Tremolo)
    public float typingSpeed = 0.05f;
}