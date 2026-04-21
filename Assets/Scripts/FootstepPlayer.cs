using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [Header("Wwise")]
    public AK.Wwise.Event footstepEvent;      // Play_Footsteps
    public AK.Wwise.Switch stoneSwitch;       // Surfaces: Stone

    [Header("Movement Settings")]
    public Rigidbody rb;                      // Drag your Rigidbody here
    public float movementThreshold = 0.1f;    // Minimum speed to count as walking
    public float stepInterval = 0.4f;         // Time between steps

    private float stepTimer = 0f;

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;

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
        // REQUIRED for Switch Containers
        stoneSwitch.SetValue(gameObject);

        // Play the event
        footstepEvent.Post(gameObject);

        Debug.Log("Footstep (Stone) played");
    }
}
