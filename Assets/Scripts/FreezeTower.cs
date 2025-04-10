using UnityEngine;

public class FreezeTower : MonoBehaviour
{
    public Transform turret;
    public GameObject slowBulletPrefab; //  Alleen SlowBullet wordt gebruikt
    public Transform[] firePoints; // dit wordt gebruikt voor verschillende FirePoints zo dat je meeder guns kan hebben
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
        if (slowBulletPrefab == null)
        {
            Debug.LogError("Geen SlowBullet prefab gekoppeld aan FreezeTower!");
            return;
        }

        foreach (Transform firePoint in firePoints)
        {
            GameObject bulletInstance = Instantiate(slowBulletPrefab, firePoint.position, firePoint.rotation);
            SlowBullet bullet = bulletInstance.GetComponent<SlowBullet>();
            if (bullet != null)
            {
                bullet.SetTarget(target);
            }
            else
            {
                Debug.LogError("De prefab bevat geen SlowBullet-script!");
            }
        }
    }
}