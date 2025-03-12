using UnityEngine;

public class EndZone : MonoBehaviour
{
    public BaseHP baseHealth;  // 🔹 Link naar de basis HP

    private void Start()
    {
        if (baseHealth == null)
        {
            baseHealth = FindObjectOfType<BaseHP>(); // 🔍 Zoek automatisch de basis
        }
    }       

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // 🔹 Controleer of het een vijand is
        {
            EnemyHP enemyHP = other.GetComponent<EnemyHP>();

            if (enemyHP != null) // ✅ Voorkomt fout als vijand geen EnemyHP-script heeft
            {
                int damage = enemyHP.GetHealth();
                baseHealth.TakeDamage(damage);
                Destroy(other.gameObject); // ❗ Vijand wordt verwijderd na de aanval
            }
            else
            {
                Debug.LogError("Enemy heeft geen EnemyHP-script! Zorg dat het script op de vijand staat.");
            }
        }
    }
}
