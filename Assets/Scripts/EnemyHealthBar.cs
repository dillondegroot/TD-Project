using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHP enemyHP;  //  ik verwijs naar het EnemyHP script
    public Image healthBarFill;  // UI-vullingsbalk

    private void Update()
    {
        if (enemyHP != null)
        {
            healthBarFill.fillAmount = (float)enemyHP.health / enemyHP.maxHealth;
        }
    }
}
