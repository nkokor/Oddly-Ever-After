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

            Gizmos.DrawSphere(waypoint.position, waypointSize);

            if (i < transform.childCount - 1)
            {
                Transform nextWaypoint = transform.GetChild(i + 1);
                Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
            }
        }

        if (transform.childCount > 1)
        {
            Transform firstWaypoint = transform.GetChild(0);
            Transform lastWaypoint = transform.GetChild(transform.childCount - 1);
            Gizmos.DrawLine(lastWaypoint.position, firstWaypoint.position);
        }
    }
}
