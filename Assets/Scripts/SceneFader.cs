using UnityEngine;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [Header("Settings")]
    public CanvasGroup curtainCanvasGroup; // Drag your Black Screen Image here

    void Start()
    {
        // 1. Ensure the curtain starts solid black
        if (curtainCanvasGroup != null)
        {
            curtainCanvasGroup.alpha = 1f;
            
            // 2. Begin fading to transparent (revealing the scene)
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float duration = 1.0f; // Adjust this to make the fade faster or slower
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // Gradually reduce alpha from 1 (Black) to 0 (Transparent)
            curtainCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }
        
        curtainCanvasGroup.alpha = 0f;
    }
}