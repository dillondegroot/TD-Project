using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHealth;
    public Image healthBarFill;
    public EnemySpawner spawner;

    private Coroutine poisonCoroutine;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyPoison(float poisonDamage, float duration, float tickInterval)
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }
        poisonCoroutine = StartCoroutine(PoisonEffect(poisonDamage, duration, tickInterval));
        
    }

    private IEnumerator PoisonEffect(float damage, float duration, float interval)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(interval);
            elapsedTime += interval;

            if (currentHealth <= 0)
            {
                yield break;
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        spawner.EnemyDied();
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
