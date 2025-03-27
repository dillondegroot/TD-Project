using UnityEngine;
using UnityEngine.UI;  // 🔹 Voor de healthbar

public class EnemyHP : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHealth;
    public Image healthBarFill; // 🔹 Verwijzing naar de healthbar

    public EnemySpawner spawner;  // 🔹 Link naar spawner (voor het tellen van vijanden)

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBarFill.fillAmount = currentHealth / maxHealth;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            spawner.EnemyDied();  // 🔹 Laat de spawner weten dat de vijand dood is
            Destroy(gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public float GetHealth() // 🔹 Nodig voor EndZone en andere scripts
    {
        return currentHealth;
    }
}
