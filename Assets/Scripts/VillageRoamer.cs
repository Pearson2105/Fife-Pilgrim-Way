using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class VillageRoamer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkRadius = 15f;
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;

    private NavMeshAgent agent;
    private Animator anim;
    private bool isWaiting = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        // 1. Wait a tiny bit for Unity to initialize physics
        yield return new WaitForSeconds(0.1f);

        // 2. Snap the NPC to the NavMesh floor to prevent the error
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        // 3. Start the roaming logic if everything is okay
        if (agent.isOnNavMesh)
        {
            StartCoroutine(RoamLoop());
        }
        else
        {
            Debug.LogError($"{gameObject.name} is not on a NavMesh! Check if the floor is blue in Scene view.");
        }
    }

    IEnumerator RoamLoop()
    {
        while (true)
        {
            // Only calculate if we are on the mesh and not already waiting
            if (agent.isOnNavMesh && !isWaiting)
            {
                // Check if we arrived at our destination
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    StartCoroutine(WaitAndPickNewTarget());
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator WaitAndPickNewTarget()
    {
        isWaiting = true;
        
        // Switch to Idle animation
        if (anim != null) anim.SetBool("isWalking", false);
        
        // Wait for a random time
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        // Pick a random spot
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1))
        {
            agent.SetDestination(hit.position);
            
            // Switch to Walk animation
            if (anim != null) anim.SetBool("isWalking", true);
        }

        isWaiting = false;
    }
}