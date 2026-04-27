using UnityEngine;
using UnityEngine.UI;
using AK.Wwise; // Makes sure we can use Wwise Types

public class CanvasSoundManager : MonoBehaviour
{
    // This will show up as a picker in your Inspector
    public AK.Wwise.Event uiClickEvent; 

    void Awake()
    {
        // Finds all buttons inside this Canvas and its Panels
        Button[] allButtons = GetComponentsInChildren<Button>(true);

        foreach (Button btn in allButtons)
        {
            // Assigns the Play_UI event to every button found
            btn.onClick.AddListener(() => {
                uiClickEvent.Post(gameObject);
            });
        }
    }
}