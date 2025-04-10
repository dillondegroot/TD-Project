using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;  // ?? Standaard snelheid
    private float normalSpeed;  // ?? Opslaan van de originele snelheid
    private bool isSlowed = false;

    private int waypointIndex = 0;

    private void Start()
    {
        normalSpeed = speed;  // ?? Bewaar de normale snelheid
        transform.position = WayPoints.waypoints[0].position;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex >= WayPoints.waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        Transform targetWaypoint = WayPoints.waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            waypointIndex++;
        }
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (!isSlowed)  // ?? Voorkom dat het effect gestapeld wordt
        {
            isSlowed = true;
            speed = (normalSpeed * multiplier);  // ?? Reken uit en rond af op een heel getal
            StartCoroutine(RemoveSlowEffect(duration));  // ?? Start de timer
        }
    }

    private IEnumerator RemoveSlowEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = normalSpeed;
        isSlowed = false;
    }
}