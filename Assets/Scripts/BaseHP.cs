using UnityEngine;

public class BaseHP : MonoBehaviour
{
    public float health = 100f;  // 🔹 Start HP van de basis
    public GameObject deathScreen;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Basis HP: " + health);

        if (health <= 0)
        {
            Debug.Log("Game Over! De basis is vernietigd.");
            deathScreen.SetActive(true);  // 🔹 Toon het game over scherm
        }
    }
}
