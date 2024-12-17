using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Transform path; 
    private Transform[] waypoints; 
    private int currentWaypointIndex = 0; 

    public float waypointTolerance = 1f; 

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Animator animator;

    public SphereCollider chaseCollider;
    public SphereCollider attackCollider;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  
    }

private void Start()
{
    if (chaseCollider != null)
    {
        chaseCollider.radius = sightRange;  
    }

    if (attackCollider != null)
    {
        attackCollider.radius = attackRange;  
    }

    if (path != null)
    {
        waypoints = new Transform[path.childCount];
        for (int i = 0; i < path.childCount; i++)
        {
            waypoints[i] = path.GetChild(i);
        }
    }
}

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && waypoints != null && waypoints.Length > 0)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        agent.SetDestination(targetWaypoint.position);

        Vector3 distanceToWaypoint = transform.position - targetWaypoint.position;
        if (distanceToWaypoint.magnitude < waypointTolerance)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player); 

        if (!alreadyAttacked)
        {
            animator.SetTrigger("IsAttacking");

            Debug.Log("Player attacked!");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (path != null)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < path.childCount; i++)
            {
                Gizmos.DrawSphere(path.GetChild(i).position, 0.5f);
            }
        }
    }
}
