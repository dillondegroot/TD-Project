using UnityEngine;

public class EndZone : MonoBehaviour
{
    public BaseHP baseHealth; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                float damage = enemyHP.GetHealth();
                baseHealth.TakeDamage(damage);

                
                if (enemyHP.spawner != null)
                {
                    enemyHP.spawner.EnemyDied();
                }

                Destroy(other.gameObject);
            }
        }
    }
}
