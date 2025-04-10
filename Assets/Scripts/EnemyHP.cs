using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum EnemyType { Nomral, MiniBoss, Boss }

public class EnemyHP : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHealth;
    public Image healthBarFill;
    public EnemySpawner spawner;

    public EnemyType enemyType;

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
        AwardMoney();
        spawner.EnemyDied();
        Destroy(gameObject);
    }

    private void AwardMoney()
    {
        int money = 0;
        switch(enemyType)
        {
            case EnemyType.Nomral: money = 10; break;
            case EnemyType.MiniBoss: money = 50; break;
            case EnemyType.Boss: money = 200; break;
        }

        PlacementScript.Instance.money += money;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}