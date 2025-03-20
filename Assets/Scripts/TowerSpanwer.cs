using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab; // Sleep je tower prefab hierin in de Inspector
    public float spawnDistance = 3f; // Afstand waar de toren spawnt voor de speler

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Druk op '1' om een toren te spawnen
        {
            SpawnTower();
        }
    }

    void SpawnTower()
    {
        if (towerPrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
            Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("⚠️ Tower prefab is niet toegewezen in de Inspector!");
        }
    }
}
