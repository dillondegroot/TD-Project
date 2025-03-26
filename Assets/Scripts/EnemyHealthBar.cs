using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarFill;  // Sleep hier de Image van de healthbar in de Inspector

    public void SetHealth(int currentHealth, int maxHealth)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
