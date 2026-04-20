using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class FactZone : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject floatingPrompt; // The "Press E" text
    public GameObject factPanel;     // The main UI Panel on screen
    public TextMeshProUGUI factText; // The actual text component on that panel

    [Header("Content")]
    [TextArea(3, 10)]
    public string historicalFact;

    private bool isPlayerInside = false;

    void Start()
    {
        floatingPrompt.SetActive(false);
        factPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            ToggleFact();
        }
    }

    void ToggleFact()
    {
        // If the panel is off, turn it on and set the text
        if (!factPanel.activeSelf)
        {
            factText.text = historicalFact;
            factPanel.SetActive(true);
            floatingPrompt.SetActive(false); // Hide prompt while reading
        }
        else
        {
            // If the panel is already on, pressing E closes it
            factPanel.SetActive(false);
            floatingPrompt.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            floatingPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            floatingPrompt.SetActive(false);
            factPanel.SetActive(false); // Close if they walk away
        }
    }
}