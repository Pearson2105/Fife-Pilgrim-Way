using UnityEngine;
using UnityEngine.AI;

public class BoarAI : MonoBehaviour
{
    public enum BoarState { Patrol, Chase, Battle }
    public BoarState currentState = BoarState.Patrol;

    [Header("Settings")]
    public float detectionRange = 10f;
    public float attackRange = 1.5f;

    [Header("References")]
    public Transform player;
    public Animator anim;
    public NavMeshAgent agent;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 1. Determine State
        if (distanceToPlayer < attackRange) currentState = BoarState.Battle;
        else if (distanceToPlayer < detectionRange) currentState = BoarState.Chase;
        else currentState = BoarState.Patrol;

        // 2. Execute State Logic
        switch (currentState)
        {
            case BoarState.Patrol:
                // We will add the waypoint logic here later
                agent.isStopped = false;
                break;
            case BoarState.Chase:
                agent.isStopped = false;
                agent.SetDestination(player.position);
                break;
            case BoarState.Battle:
                agent.isStopped = true;
                break;
        }

        // 3. Sync Animator
        anim.SetInteger("BoarState", (int)currentState);
    }
}