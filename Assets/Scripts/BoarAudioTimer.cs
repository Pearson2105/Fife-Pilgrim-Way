using UnityEngine;

public class BoarAudioTimer : MonoBehaviour
{
    [SerializeField] private string eventName = "Play_Wild_Boar";
    [SerializeField] private float interval = 5.0f;
    [SerializeField] private float randomness = 1.0f; // Adds variety

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            PlayBoarSound();
            ResetTimer();
        }
    }

    void PlayBoarSound()
    {
        // This triggers the Wwise event on the Boar
        AkSoundEngine.PostEvent(eventName, gameObject);
    }

    void ResetTimer()
    {
        // Sets the timer to 5 seconds +/- the randomness factor
        timer = interval + Random.Range(-randomness, randomness);
    }
}