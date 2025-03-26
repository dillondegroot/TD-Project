using UnityEngine;
using UnityEngine.UI;  // Nodig voor UI-elementen

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public EnemySpawner spawner;  // Verwijzing naar de spawner
    private EnemyHealthBar healthBar; // Verwijzing naar de healthbar

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>(); // Zoekt de HealthBar in de vijand
        UpdateHealthBar();  // Zorgt dat de healthbar juist begint
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();  // Healthbar aanpassen bij schade

        if (currentHealth <= 0)
        {
            spawner.EnemyDied();  // Laat de spawner weten dat deze vijand is gestorven
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth); // Stuur HP info naar de EnemyHealthBar
        }
    }

    public int GetHealth()
    {
        return currentHealth;  // Zorgt dat EndZone de HP kan opvragen
    }
}
