using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public DialogueData myDialogue; 
    public AnimalCrossingDialogue system; 
    public float range = 3f;
    public Transform player;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            if (Input.GetKeyDown(KeyCode.E)) // Press E to talk
            {
                system.PlayCard(myDialogue);
            }
        }
    }
}