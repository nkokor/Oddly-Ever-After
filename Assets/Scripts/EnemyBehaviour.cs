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
    public float timeBetweenAttacks = 4f;
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
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null && playerController.isInvulnerable)
        {
            currentState = State.Patrolling; 
        }
        else
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
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null && !playerController.isInvulnerable) 
        {
            agent.SetDestination(player.position);
        }
        else
        {
            currentState = State.Patrolling; 
        }
    }

    private void AttackPlayer()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null && !playerController.isInvulnerable) 
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("IsAttacking");
            SoundManager.Instance.PlaySound3D("Ghost Scare", transform.position);

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

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
