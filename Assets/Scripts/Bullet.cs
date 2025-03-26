using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 3;  // ?? Zorgt ervoor dat de toren 3 schade doet
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // ?? Beweeg de kogel richting de vijand
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        EnemyHP enemyHp = target.GetComponent<EnemyHP>();
        if (enemyHp != null)
        {
            enemyHp.TakeDamage(damage);  // ?? Breng schade toe aan vijand
        }

        Destroy(gameObject); // ?? Kogel verdwijnt na raken
    }
}
