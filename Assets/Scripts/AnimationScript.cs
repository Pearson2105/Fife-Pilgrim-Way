using UnityEngine;

public class BoarController : MonoBehaviour
{
    [Header("Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionRange = 7f;
    public float attackRange = 1.0f;

    [Header("References")]
    public Transform player;
    public Transform[] waypoints;
    public GameObject battleCanvas;
    public Animator anim;

    private int currentWaypointIndex = 0;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            TriggerBattle();
        }
        else if (distanceToPlayer < detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            PatrolPath();
        }
    }

    void PatrolPath()
    {
        anim.SetBool("isChasing", false);
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void ChasePlayer()
    {
        anim.SetBool("isChasing", true);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        transform.LookAt(player);
    }

    void TriggerBattle()
    {
        battleCanvas.SetActive(true);
        Time.timeScale = 0f;
        enabled = false;
    }

    public void Die()
    {
        anim.SetTrigger("Die");
        enabled = false;
        Destroy(gameObject, 2.0f);
    }
}