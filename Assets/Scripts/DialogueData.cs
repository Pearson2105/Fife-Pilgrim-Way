using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Card")]
public class DialogueData : ScriptableObject
{
    public string characterName;
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Wwise Audio")]
    public string voiceType;       // e.g., "Priest"
    public string switchGroup = "VoiceType";
    public float genderPitch = 0f;
    public float voiceWobble = 0f;
    public float typingSpeed = 0.05f;
}
