using System.Collections;
using UnityEngine;

public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;
    public float duration = 1f;

    public override IEnumerator AnimateTransitionIn()
    {
        yield return Fade(0f, 1f);
    }

    public override IEnumerator AnimateTransitionOut()
    {
        yield return Fade(1f, 0f);
    }

    private IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;
        crossFade.alpha = start;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            crossFade.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        crossFade.alpha = end;
    }
}