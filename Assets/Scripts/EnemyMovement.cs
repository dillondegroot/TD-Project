using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private int waypointIndex = 0;

    private void Start()
    {
        if (WayPoints.waypoints == null || WayPoints.waypoints.Length == 0)
        {
            Debug.LogError("Geen waypoints gevonden! Zorg ervoor dat WaypointManager goed is ingesteld.");
            return;
        }

        transform.position = WayPoints.waypoints[0].position; // Start op eerste waypoint
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex >= WayPoints.waypoints.Length)
        {
            Destroy(gameObject); // Verwijder vijand als hij het einde bereikt
            return;
        }

        Transform targetWaypoint = WayPoints.waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            waypointIndex++; // Ga naar het volgende waypoint
        }
    }
}
