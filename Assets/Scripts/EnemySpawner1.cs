using System.Collections;
using UnityEngine;
using TMPro; // gebruikt TMPro ander werkt text niet

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // 🔹 Normale vijanden
    public GameObject miniBossPrefab;  // 🔹 Miniboss prefab
    public TMP_Text waveText;  // 🔹 UI weergave van de wave

    public float spawnRate = 1.5f;
    public int enemiesPerWave = 5;
    public float waveDelay = 5f;
    public int totalWaves = 5;
    public bool endlessWaves = false;

    private int waveNumber = 1;
    private int enemiesAlive = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (endlessWaves || waveNumber <= totalWaves)
        {
            if (waveText != null)
            {
                waveText.text = "Wave: " + waveNumber;
            }

            Debug.Log("Wave " + waveNumber + " gestart!");
            enemiesAlive = 0;

            if (waveNumber == 5)
            {
                SpawnMiniBoss();
            }
            else
            {
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnRate);
                }
            }

            yield return new WaitUntil(() => enemiesAlive <= 0);

            waveNumber++;
            yield return new WaitForSeconds(waveDelay);
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
        Vector3 spawnOffset = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

        GameObject enemyClone = Instantiate(enemyToSpawn, WayPoints.waypoints[0].position + spawnOffset, Quaternion.identity);
        enemyClone.name = enemyToSpawn.name + " Clone";

        enemiesAlive++;
        enemyClone.GetComponent<EnemyHP>().spawner = this;
    }

    private void SpawnMiniBoss()
    {
        GameObject miniBoss = Instantiate(miniBossPrefab, WayPoints.waypoints[0].position, Quaternion.identity);
        miniBoss.name = "MiniBoss Clone";
        enemiesAlive++;
        miniBoss.GetComponent<EnemyHP>().spawner = this;
    }

    private GameObject SelectEnemy()
    {
        if (waveNumber < 3)
        {
            return enemyPrefabs[0];
        }
        else if (waveNumber < 5)
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }
        else
        {
            return null;
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }

    // 🔹 Deze methode maakt het mogelijk om de huidige wave op te vragen
    public int GetWaveNumber()
    {
        return waveNumber;
    }
}
