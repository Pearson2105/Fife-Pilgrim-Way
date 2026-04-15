using UnityEngine;

public class SceneMusic_Pilgrim : MonoBehaviour
{
    public AK.Wwise.Event Play_Pilgrim_Music;
    public AK.Wwise.Event Stop_Pilgrim_Music;

    private void Start()
    {
        if (Play_Pilgrim_Music != null)
        {
            Play_Pilgrim_Music.Post(gameObject);
        }
        else
        {
            Debug.LogError("Play_Pilgrim_Music event is not assigned.");
        }
    }

    private void OnDestroy()
    {
        if (Stop_Pilgrim_Music != null)
        {
            Stop_Pilgrim_Music.Post(gameObject);
        }
    }
}

