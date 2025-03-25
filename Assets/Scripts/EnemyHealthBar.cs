using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarFill;  // De UI-balk die de HP toont
    private EnemyHP enemyHP;  // Maak het een private variabele

    private void Start()
    {
        enemyHP = GetComponentInParent<EnemyHP>(); // Zoek automatisch het script
    }

    private void Update()
    {
        if (enemyHP != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)enemyHP.GetHealth() / enemyHP.maxHealth;
        }
    }
}
