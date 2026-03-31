using UnityEngine;
using UnityEngine.InputSystem; // Uses the New Input System

public class PlayerController3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 7f;
    public float mouseSensitivity = 2f;
    
    [Header("References")]
    public DialogueManager dialogueManager; 
    public Transform cameraTransform; // Drag your Main Camera here

    private Rigidbody rb;
    private Vector2 moveInput;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // 1. Setup Physics: Freeze rotation so the capsule doesn't tip over
        rb.freezeRotation = true;
        rb.useGravity = true;

        // 2. Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- 3. INPUT SYSTEM CHECK ---
        if (Keyboard.current == null || Mouse.current == null) return;

        // --- 4. DIALOGUE FREEZE ---
        // If the Priest is talking, we stop all movement logic
        if (dialogueManager != null && dialogueManager.IsTalking())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // Keep cursor locked while playing
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // --- 5. MOUSE LOOK (Turning) ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        // Horizontal: Rotate the whole Capsule left/right
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity * 0.1f);

        // Vertical: Rotate ONLY the camera up/down
        verticalRotation -= mouseDelta.y * mouseSensitivity * 0.1f;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }

        // --- 6. KEYBOARD INPUT (WASD + Arrows) ---
        float x = 0;
        float z = 0;

        // Forward/Back
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) z = 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) z = -1;

        // Left/Right
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1;

        moveInput = new Vector2(x, z).normalized;
    }

    void FixedUpdate()
    {
        // --- 7. APPLY PHYSICS MOVEMENT ---
        // We transform the input direction to be relative to where the player is facing
        Vector3 direction = (transform.right * moveInput.x) + (transform.forward * moveInput.y);
        
        Vector3 targetVelocity = direction * moveSpeed;
        targetVelocity.y = rb.linearVelocity.y; // Keep current gravity speed

        rb.linearVelocity = targetVelocity;
    }
}