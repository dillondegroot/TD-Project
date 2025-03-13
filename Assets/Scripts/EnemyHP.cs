using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int health = 10;
    public EnemySpawner spawner;  // 🔹 Verwijzing naar de spawner

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            spawner.EnemyDied();  // 🔹 Vertel de spawner dat deze vijand is gestorven
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
