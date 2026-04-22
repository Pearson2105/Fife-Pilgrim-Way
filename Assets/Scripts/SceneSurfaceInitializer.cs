using UnityEngine;

public class SceneSurfaceInitializer : MonoBehaviour
{
    [Header("Wwise Settings")]
    public AK.Wwise.Switch surfaceSwitch; // Assign "Grass" in the Grass scene, "Stone" in the Stone scene

    void Start()
    {
        if (surfaceSwitch.IsValid())
        {
            // Sets the surface for the Player globally in this scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            if (player != null)
            {
                surfaceSwitch.SetValue(player);
                Debug.Log($"Wwise Surface initialized to: {surfaceSwitch.Name} for {player.name}");
            }
        }
    }
}