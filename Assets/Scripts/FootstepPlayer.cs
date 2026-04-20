using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Wwise")]
    public AK.Wwise.Event footstepEvent;      // Play_Footsteps
    public AK.Wwise.Switch stoneSwitch;       // Surfaces: Stone
    public AK.Wwise.Switch grassSwitch;       // Surfaces: Grass

    [Header("Movement Settings")]
    public Rigidbody rb;                      // Assign your Rigidbody
    public float movementThreshold = 0.1f;    // Minimum speed to count as walking
    public float stepInterval = 0.4f;         // Time between steps

    private float stepTimer = 0f;

    // Default surface
    public string currentSurface = "Stone";

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
        // Set correct surface switch
        if (currentSurface == "Grass")
            grassSwitch.SetValue(gameObject);
        else
            stoneSwitch.SetValue(gameObject);

        // Play the event
        footstepEvent.Post(gameObject);

        Debug.Log("Footstep: " + currentSurface);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass"))
            currentSurface = "Grass";

        if (other.CompareTag("Stone"))
            currentSurface = "Stone";
    }
}
