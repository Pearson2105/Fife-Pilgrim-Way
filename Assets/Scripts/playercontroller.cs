using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float sensitivity = 2f;
    public Transform cam; // Drag your Main Camera here

    private Rigidbody rb;
    private float vRotation = 0f;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Prevent the capsule from falling over
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. READ MOUSE
        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            transform.Rotate(Vector3.up * mouseDelta.x * sensitivity * 0.1f);

            vRotation -= mouseDelta.y * sensitivity * 0.1f;
            vRotation = Mathf.Clamp(vRotation, -80f, 80f);
            if (cam != null) cam.localRotation = Quaternion.Euler(vRotation, 0, 0);
        }

        // 2. READ KEYS
        if (Keyboard.current != null)
        {
            float x = 0; float z = 0;
            if (Keyboard.current.wKey.isPressed) z = 1;
            if (Keyboard.current.sKey.isPressed) z = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;
            moveInput = new Vector2(x, z).normalized;
        }
    }

    void FixedUpdate()
    {
        // 3. APPLY PHYSICS (This is the gravity fix!)
        Vector3 moveDir = (transform.right * moveInput.x) + (transform.forward * moveInput.y);
        
        // We set the X and Z speed, but we TAKE the current Y speed from the physics engine
        // This allows gravity to pull the player down properly.
        Vector3 currentVelocity = rb.linearVelocity; 
        Vector3 targetVelocity = new Vector3(moveDir.x * moveSpeed, currentVelocity.y, moveDir.z * moveSpeed);
        
        rb.linearVelocity = targetVelocity;
    }
}