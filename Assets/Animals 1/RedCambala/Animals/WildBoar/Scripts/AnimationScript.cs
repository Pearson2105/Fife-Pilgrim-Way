using UnityEngine;

public class BoarAnimation : MonoBehaviour
{
    private Animator animator;
    private int currentBoarState = -1; // Keep track of what is playing

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check keys only once per press
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeState(0); // Idle
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeState(1); // Run
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeState(2); // Attack1
        if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeState(3); // Death1
    }

    void ChangeState(int newState)
    {
        // Only update if it's a new state to stop the glitching/looping
        if (currentBoarState != newState)
        {
            currentBoarState = newState;
            animator.SetInteger("BoarState", newState);
        }
    }
}