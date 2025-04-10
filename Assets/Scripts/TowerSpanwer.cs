using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject[] towerPrefabs; 
    public float spawnDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnTower(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnTower(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnTower(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SpawnTower(3);
    }

    void SpawnTower(int index)
    {
        if (index >= 0 && index < towerPrefabs.Length && towerPrefabs[index] != null)
        {
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
            Instantiate(towerPrefabs[index], spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("⚠️ Tower prefab " + index + " is niet ingesteld!");
        }
    }
}
