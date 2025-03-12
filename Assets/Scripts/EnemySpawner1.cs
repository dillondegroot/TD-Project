using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // 🔹 Verschillende vijand-types (laatste prefab = mini-boss)
    public float spawnRate = 1.5f;
    public int enemiesPerWave = 5;
    public float waveDelay = 5f;
    public int totalWaves = 5;
    public bool endlessWaves = false;

    private int waveNumber = 1;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (endlessWaves || waveNumber <= totalWaves)
        {
            Debug.Log("Wave " + waveNumber + " gestart!");

            int enemyCount = (waveNumber == 5) ? 1 : enemiesPerWave; // 🔹 Wave 5: Alleen 1 mini-boss

            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnRate);
            }

            yield return new WaitForSeconds(waveDelay);
            waveNumber++;  // 🔹 Pas verhogen na de wachttijd
        }

        Debug.Log("Alle waves voltooid!");
    }

    private void SpawnEnemy()
    {
        if (WayPoints.waypoints == null || WayPoints.waypoints.Length == 0)
        {
            Debug.LogError("Geen waypoints gevonden! Vijanden kunnen niet spawnen.");
            return;
        }

        GameObject enemyToSpawn = SelectEnemy();

        if (enemyToSpawn == null) return; // 🔹 Stop als er geen vijand geselecteerd is

        Vector3 spawnOffset = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        GameObject enemyClone = Instantiate(enemyToSpawn, WayPoints.waypoints[0].position + spawnOffset, Quaternion.identity);
        enemyClone.name = enemyToSpawn.name + " Clone";

       // Debug.Log("Enemy spawned: " + enemyClone.name);
    }

    private GameObject SelectEnemy()
    {
        if (waveNumber == 5)
        {
            return enemyPrefabs[enemyPrefabs.Length - 1]; // 🔹 Laatste prefab = mini-boss
        }
        else if (waveNumber < 3)
        {
            return enemyPrefabs[0]; // 🔹 Normale vijand voor wave 1-2
        }
        else
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)]; // 🔹 Willekeurig, behalve de mini-boss
        }
    }
}
