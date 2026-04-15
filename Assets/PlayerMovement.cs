using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Reset this to 10 (100 is too fast for velocity!)
    public float lookSensitivity = 0.5f;

    [Header("References")]
    public Rigidbody rb;
    public Transform cameraTransform;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private float cameraRotationX = 0f;

    void Start()
    {
        
        // Lock the cursor so it doesn't click outside the game window
        Cursor.lockState = CursorLockMode.Locked;

        // Auto-assign Rigidbody if you forgot to drag it in
        if (rb == null) rb = GetComponent<Rigidbody>();

        rb.isKinematic = false; 
        rb.useGravity = true;
    }

    // This matches the "OnMove" message from your Player Input component
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // This matches the "OnLook" message from your Player Input component
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        // --- MOUSE LOOK (ROTATION) ---
        
        // Rotate the player body left/right
        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);

        // Tilt the camera up/down
        cameraRotationX -= lookInput.y * lookSensitivity;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f); 
        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

    void FixedUpdate()
    {
        // --- MOVEMENT (VELOCITY) ---
        
        // Calculate the direction based on where the player is looking
        Vector3 moveDir = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;
        
        // We set the X and Z speed, but we keep the Y speed (so gravity still works!)
        rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
    }
}