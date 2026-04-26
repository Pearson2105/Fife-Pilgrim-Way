using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float lookSensitivity = 0.5f;

    [Header("References")]
    public Rigidbody rb;
    public Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float cameraRotationX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (rb == null) rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    // --- INPUT SYSTEM ---
    public void OnMove(InputValue value)
    {
        if (PauseMenu.GameIsPaused) return;
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (PauseMenu.GameIsPaused) return;
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        // --- MOUSE LOOK ---
        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);

        cameraRotationX -= lookInput.y * lookSensitivity;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

    void FixedUpdate()
    {
        if (PauseMenu.GameIsPaused)
        {
            // Stop movement while paused
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        // --- MOVEMENT ---
        Vector3 moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
    }
}