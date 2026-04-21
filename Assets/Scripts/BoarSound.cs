using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class WildBoarSound : MonoBehaviour
{
    [SerializeField] private EventReference wildBoarEvent;

    [Header("Timing Settings")]
    [SerializeField] private float minDelay = 5f;   // minimum seconds between attempts
    [SerializeField] private float maxDelay = 12f;  // maximum seconds between attempts
    [SerializeField] private float chanceToPlay = 0.3f; // 30% chance each cycle

    private float timer;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TryPlaySound();
            ResetTimer();
        }
    }

    private void TryPlaySound()
    {
        if (Random.value <= chanceToPlay)
        {
            RuntimeManager.PlayOneShot(wildBoarEvent, transform.position);
        }
    }

    private void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
