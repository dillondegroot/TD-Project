using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 3f;  // ?? Schade per kogel
    private Transform target;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // ?? Vernietig de kogel als er geen doel meer is
            return;
        }

        // ?? Beweeg naar het doelwit
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // ?? Controleer of de kogel het doel raakt
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
            enemyHP.TakeDamage(damage);  // ?? Brengt schade toe aan de vijand
        }
        Destroy(gameObject); // ?? Vernietig de kogel na impact
    }
}
