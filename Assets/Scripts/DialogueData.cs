using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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


// ------------------------------------------------------------
// Dialogue Display (Typing + Skip with Spacebar)
// ------------------------------------------------------------
public class DialogueDisplay : MonoBehaviour
{
    public Text dialogueText;
    public DialogueData dialogueData;

    private int currentLine = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private string fullLine; // stores the full line being typed

    void Start()
    {
        ShowNextLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // SKIP → instantly show full line
                StopCoroutine(typingCoroutine);
                dialogueText.text = fullLine;
                isTyping = false;
            }
            else
            {
                // NEXT LINE
                ShowNextLine();
            }
        }
    }

    void ShowNextLine()
    {
        if (currentLine >= dialogueData.dialogueLines.Length)
        {
            dialogueText.text = "";
            return;
        }

        fullLine = dialogueData.dialogueLines[currentLine];
        typingCoroutine = StartCoroutine(TypeLine(fullLine));
        currentLine++;
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;
    }
}
