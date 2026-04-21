using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    [Header("Wwise")]
    public AK.Wwise.Event ambienceEvent;   // Assign Play_Birds_and_Wind_01

    void Start()
    {
        if (ambienceEvent != null)
        {
            ambienceEvent.Post(gameObject);
            Debug.Log("Ambience started: Play_Birds_and_Wind_01");
        }
        else
        {
            Debug.LogWarning("Ambience event not assigned!");
        }
    }
}
