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
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void OnLook(InputValue value) => lookInput = value.Get<Vector2>();

    void Update()
    {
        // Only allow camera rotation when game is not paused
        if (Time.timeScale > 0 && !PauseMenu.GameIsPaused)
        {
            // Mouse Look
            transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);

            cameraRotationX -= lookInput.y * lookSensitivity;
            cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);

            if (cameraTransform != null)
                cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
        }
    }

    void FixedUpdate()
    {
        // Only allow movement when game is not paused
        if (Time.timeScale > 0 && !PauseMenu.GameIsPaused)
        {
            Vector3 moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

            rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        }
        else
        {
            // Stop horizontal movement when paused (keep gravity/Y velocity)
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}