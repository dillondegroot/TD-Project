using UnityEngine;

public class BaseHP : MonoBehaviour
{
    public int health = 100;  // 🔹 Start HP van de basis

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Basis HP: " + health);

        if (health <= 0)
        {
            Debug.Log("Game Over! De basis is vernietigd.");
            // ❗ Voeg hier je game-over logica toe
        }
    }
}
