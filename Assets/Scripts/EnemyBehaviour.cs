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
    private float timeBetweenAttacks = 4f;
    private float chaseRange = 3f;
    private float attackRange = 1.3f;

    private Animator animator;
    private bool alreadyAttacked;

    private enum State { Patrolling, Chasing, Attacking }
    private State currentState;

    private Quaternion initialRotation;

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
        initialRotation = transform.rotation;
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
            RotateTowardsPlayer();
            agent.SetDestination(transform.position); 
            animator.SetTrigger("IsAttacking");
            SoundManager.Instance.PlaySound3D("Ghost Scare", transform.position);

            if (!alreadyAttacked)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                playerController.TriggerFall();
                Invoke(nameof(RotateBackToInitial), 0.5f);

                StartCoroutine(StopMovementForAttack());
            }
        }
    }

    private IEnumerator StopMovementForAttack()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(2.5f); 
        agent.isStopped = false;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void RotateBackToInitial()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
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
