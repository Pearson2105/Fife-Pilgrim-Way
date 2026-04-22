using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [Header("Wwise Settings")]
    public AK.Wwise.Event footstepEvent; // This defines 'footstepEvent'
    public AK.Wwise.Switch surfaceSwitch; 

    [Header("Movement Settings")]
    public Rigidbody rb;
    public float movementThreshold = 0.1f;
    public float stepInterval = 0.4f;

    private float stepTimer = 0f;

    void Update()
    {
        // Unity 6 uses linearVelocity for Rigidbodies
        if (rb == null) return;

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
        // 1. Set the surface (Stone or Grass)
        if (surfaceSwitch != null && surfaceSwitch.IsValid())
        {
            surfaceSwitch.SetValue(gameObject);
        }

        // 2. Play the event
        if (footstepEvent != null && footstepEvent.IsValid())
        {
            footstepEvent.Post(gameObject);
            Debug.Log("Playing Footstep on: " + surfaceSwitch.Name);
        }
        else
        {
            Debug.LogError("FootstepEvent is missing! Check the Inspector.");
        }
    }
}