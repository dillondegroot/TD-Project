using UnityEngine;

public class PoisonBullet : MonoBehaviour
{
    public float speed = 10f;
    public float damagePerTick = 1f;
    public float duration = 15f;
    public float tickRate = 1f;

    private Transform target;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        EnemyHP enemyHP = target.GetComponent<EnemyHP>();
        if (enemyHP != null)
        {
            enemyHP.ApplyPoison(damagePerTick, duration, tickRate);
        }

        Destroy(gameObject, 1.5f); 
    }
}
