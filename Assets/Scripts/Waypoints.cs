using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] waypoints;

    private void Awake()
    {
        if (transform.childCount == 0)
        {
            Debug.LogError("WaypointManager heeft geen waypoints! Voeg child objects toe.");
            return;
        }

        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
