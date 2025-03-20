using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10; // 🔹 Nieuw toegevoegd voor correcte health bar
    public EnemySpawner spawner;  // 🔹 Verwijzing naar de spawner

    private void Start()
    {
        health = maxHealth; // 🔹 Zorgt ervoor dat vijanden starten met maximale HP
    }

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
