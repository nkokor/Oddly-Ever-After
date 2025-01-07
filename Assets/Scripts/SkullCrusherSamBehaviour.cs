using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkullcrusherSamBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform path;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public float waypointTolerance = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
    }

    private void Update()
    {
        Patroling();
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

        bool isMoving = agent.velocity.magnitude > 0.1f;
    }

    private void OnDrawGizmosSelected()
    {
        if (path != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.childCount; i++)
            {
                Transform waypoint = path.GetChild(i);
                Gizmos.DrawSphere(waypoint.position, 0.2f);

                if (i < path.childCount - 1)
                {
                    Gizmos.DrawLine(waypoint.position, path.GetChild(i + 1).position);
                }
                else
                {
                    Gizmos.DrawLine(waypoint.position, path.GetChild(0).position);
                }
            }
        }
    }
}
