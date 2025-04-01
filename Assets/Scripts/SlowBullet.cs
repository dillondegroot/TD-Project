using UnityEngine;

public class SlowBullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 3f;  // ?? Schade per kogel
    public float slowMultiplier = 0.5f;  // ?? Vertraagt vijand naar 50% van zijn snelheid
    public float slowDuration = 5f;  // ?? Hoe lang de vertraging duurt

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
        EnemyMovement enemyMovement = target.GetComponent<EnemyMovement>(); // ?? Verwijzing naar de movement

        if (enemyHP != null)
        {
            enemyHP.TakeDamage(Mathf.RoundToInt(damage));  // ?? Rond schade af op een heel getal
        }

        if (enemyMovement != null)
        {
            enemyMovement.ApplySlow(slowMultiplier, slowDuration); // ?? Roep de slow functie aan
        }

        Destroy(gameObject); // ?? Vernietig de kogel na impact
    }
}
