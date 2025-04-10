using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform turret; // ?? Verwijzing naar het turret-object (kanon)
    public GameObject bulletPrefab; // ?? De kogel die wordt afgevuurd
    public Transform[] firePoints; // ?? Meerdere vuurpunten
    public float range = 10f; // ?? Hoe ver de toren vijanden kan zien
    public float fireRate = 1f; // ?? Hoe vaak de toren schiet
    private float fireCooldown = 0f;

    private Transform target;

    private void Update()
    {
        FindTarget();
        if (target != null)
        {
            RotateTurret(); // ?? Laat het kanon draaien naar de vijand
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
            fireCooldown -= Time.deltaTime;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void RotateTurret()
    {
        if (target == null) return;

        Vector3 direction = target.position - gameObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f); // ?? Alleen draaien over de Y-as
    }

    void Shoot()
    {
        if (target == null) return;

        foreach (Transform firePoint in firePoints) // ?? Schiet vanuit alle vuurpunten
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().SetTarget(target);
        }
    }
}
