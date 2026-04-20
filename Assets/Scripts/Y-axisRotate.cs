using UnityEngine;

public class BillboardManager : MonoBehaviour
{
    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        foreach (Transform child in transform)
        {
            Vector3 targetPosition = cam.transform.position;
            targetPosition.y = child.position.y;
            
            // The "Vector3.up" tells Unity to look using the global up direction
            // instead of the parent's orientation.
            child.LookAt(targetPosition, Vector3.up);
            
            // Flip to face the camera
            child.localRotation *= Quaternion.Euler(0, 180, 0); 
        }
    }
}