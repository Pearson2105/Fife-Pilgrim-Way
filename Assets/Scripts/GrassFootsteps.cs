using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [Header("Wwise")]
    public AK.Wwise.Event footstepEvent;   // This MUST show in Inspector

    [Header("Movement Settings")]
    public Rigidbody rb;
    public float movementThreshold = 0.1f;
    public float stepInterval = 0.4f;

    private float stepTimer = 0f;

    void Update()
    {
        float speed = rb.velocity.magnitude;

        if (speed > movementThreshold)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepEvent != null)
        {
            footstepEvent.Post(gameObject);
            Debug.Log("Grass footstep played");
        }
        else
        {
            Debug.LogWarning("Footstep event not assigned!");
        }
    }
}
