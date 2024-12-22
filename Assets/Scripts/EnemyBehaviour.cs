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
    public float timeBetweenAttacks = 2f;
    public float chaseRange = 4f; 
    public float attackRange = 2f; 

    private Animator animator;
    private bool alreadyAttacked;

    private enum State { Patrolling, Chasing, Attacking }
    private State currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  
    }

    private void Start()
    {
        if (path != null)
        {
            waypoints = new Transform[path.childCount];
            for (int i = 0; i < path.childCount; i++)
            {
                waypoints[i] = path.GetChild(i);
            }
        }

        currentState = State.Patrolling; 
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attacking;
        }
        else if (distanceToPlayer <= chaseRange)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Patrolling;
        }

        HandleState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patroling();
                break;
            case State.Chasing:
                ChasePlayer();
                break;
            case State.Attacking:
                AttackPlayer();
                break;
        }
    }

    private void Patroling()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        agent.SetDestination(targetWaypoint.position);

        Vector3 distanceToWaypoint = transform.position - targetWaypoint.position;
        if (distanceToWaypoint.magnitude < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animator.SetTrigger("IsAttacking");

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TriggerFall(); 
            }
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
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
