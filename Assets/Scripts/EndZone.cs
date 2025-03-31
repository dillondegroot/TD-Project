using UnityEngine;

public class EndZone : MonoBehaviour
{
    public BaseHP baseHealth;  // Link naar de basis

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                float damage = enemyHP.GetHealth();
                baseHealth.TakeDamage(damage);

                // 🔹 Laat de spawner weten dat de vijand weg is
                if (enemyHP.spawner != null)
                {
                    enemyHP.spawner.EnemyDied();
                }

                Destroy(other.gameObject);
            }
        }
    }
}
