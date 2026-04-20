using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float scrollX = 0.05f;
    public float scrollY = 0.05f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate offset based on time and speed
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        Vector2 offset = new Vector2(offsetX, offsetY);

        // Try to apply to URP property names first
        if (rend.material.HasProperty("_BaseMap"))
        {
            rend.material.SetTextureOffset("_BaseMap", offset);
        }
        // Then try Standard shader names
        else if (rend.material.HasProperty("_MainTex"))
        {
            rend.material.SetTextureOffset("_MainTex", offset);
        }

        // Apply to the Normal Map (the waves)
        if (rend.material.HasProperty("_BumpMap"))
        {
            rend.material.SetTextureOffset("_BumpMap", offset);
        }
    }
}