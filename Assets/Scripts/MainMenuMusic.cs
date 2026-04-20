using UnityEngine;

public class WwiseMusicManager : MonoBehaviour
{
    [Header("Wwise Events")]
    public AK.Wwise.Event playEvent;
    public AK.Wwise.Event stopEvent;

    [Header("Optional Fade Control")]
    public AK.Wwise.RTPC musicVolumeRTPC;

    private bool isPlaying = false;

    private void Start()
    {
        PlayMusic();
    }

    private void OnDestroy() => StopMusic();   // ← ADDED LINE

    public void PlayMusic()
    {
        if (playEvent == null)
        {
            Debug.LogError("Play event is not assigned in the Inspector.");
            return;
        }

        playEvent.Post(gameObject);
        isPlaying = true;
    }

    public void StopMusic()
    {
        if (stopEvent == null)
        {
            Debug.LogError("Stop event is not assigned in the Inspector.");
            return;
        }

        stopEvent.Post(gameObject);
        isPlaying = false;
    }

    public void FadeOut(float fadeTime = 2f)
    {
        if (musicVolumeRTPC == null)
        {
            Debug.LogWarning("No RTPC assigned for fading.");
            StopMusic();
            return;
        }

        StartCoroutine(FadeOutRoutine(fadeTime));
    }

    private System.Collections.IEnumerator FadeOutRoutine(float time)
    {
        float t = 0f;

        while (t < time)
        {
            float value = Mathf.Lerp(100f, 0f, t / time);
            musicVolumeRTPC.SetValue(gameObject, value);
            t += Time.deltaTime;
            yield return null;
        }

        musicVolumeRTPC.SetValue(gameObject, 0f);
        StopMusic();
    }
}
