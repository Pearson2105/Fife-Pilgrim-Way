using UnityEngine;

// This line allows you to right-click in your Project folder 
// to create new dialogue files (Create > Dialogue System > Dialogue Card)
[CreateAssetMenu(fileName = "NewDialogueCard", menuName = "Dialogue System/Dialogue Card")]
public class DialogueData : ScriptableObject
{
    [Header("Character Info")]
    public string characterName = "Priest"; // The name shown in the UI
    
    

    [Header("Dialogue Lines")]
    [TextArea(3, 10)] // This makes the text box bigger and easier to type in
    public string[] lines;
}