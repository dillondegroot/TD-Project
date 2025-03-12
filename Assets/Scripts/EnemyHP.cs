using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int health = 10; // 🔹 Start HP van de vijand

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " HP: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetHealth()  // 🔹 Geeft de resterende HP terug
    {
        return Mathf.Max(health, 0); // Voorkomt negatieve waarden
    }
}
