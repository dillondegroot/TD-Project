using UnityEngine;

public class PoisonTower : MonoBehaviour
{
    public Transform turret;
    public GameObject PoisonBulletPrefab;
    public Transform[] firePoints;
    public float range = 10f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    private Transform target;

    private void Update()
    {
        FindTarget();
        if (target != null)
        {
            RotateTurret();
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

        target = nearestEnemy != null ? nearestEnemy.transform : null;
    }

    void RotateTurret()
    {
        if (target == null) return;

        Vector3 direction = target.position - turret.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(turret.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        turret.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        if (PoisonBulletPrefab == null)
        {
            Debug.LogError("Geen PoisonBullet prefab gekoppeld aan PoisonTower!");
            return;
        }

        if (firePoints.Length == 0)
        {
            Debug.LogError("Geen firePoints ingesteld op PoisonTower!");
            return;
        }

        foreach (Transform firePoint in firePoints)
        {
            GameObject bulletP = Instantiate(PoisonBulletPrefab, firePoint.position, firePoint.rotation);
            PoisonBullet bullet = bulletP.GetComponent<PoisonBullet>();
            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
            else
            {
                Debug.LogError("De prefab bevat geen PoisonBullet-script!");
            }
        }
    }
}
