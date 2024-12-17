using UnityEngine;

[ExecuteAlways] 
public class PathVisualizer : MonoBehaviour
{
    public Color gizmoColor = Color.red; 
    public float waypointSize = 0.3f;   

private void OnDrawGizmos()
{
    Gizmos.color = gizmoColor;

    for (int i = 0; i < transform.childCount; i++)
    {
        Transform waypoint = transform.GetChild(i);

        // Draw a sphere at the waypoint position
        Gizmos.DrawSphere(waypoint.position, waypointSize);

        // Draw a line to the next waypoint
        if (i < transform.childCount - 1)
        {
            Transform nextWaypoint = transform.GetChild(i + 1);
            Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
        }
    }

    // Connect the last waypoint back to the first one to form a loop
    if (transform.childCount > 1)
    {
        Transform firstWaypoint = transform.GetChild(0);
        Transform lastWaypoint = transform.GetChild(transform.childCount - 1);
        Gizmos.DrawLine(lastWaypoint.position, firstWaypoint.position);
    }
}


}
