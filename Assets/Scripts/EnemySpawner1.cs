using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject miniBossPrefab;
    public GameObject bossPrefab;
    public TMP_Text waveText;

    public float spawnRate = 1.5f;
    public int enemiesPerWave = 5;
    public float waveDelay = 5f;
    public int maxWaves = 20;
    public bool endlessWaves = false;

    private int waveNumber = 1;
    private int enemiesAlive = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (endlessWaves || waveNumber <= maxWaves)
        {
            if (waveText != null)
            {
                waveText.text = "Wave: " + waveNumber + " / " + maxWaves;
            }

            Debug.Log("Wave " + waveNumber + " gestart!");
            enemiesAlive = 0;

            // Wave 20: eindbaas + 6 minibosses, geen gewone enemies
            if (waveNumber == 20)
            {
                SpawnBoss();
                for (int i = 0; i < 6; i++)
                {
                    SpawnMiniBoss();
                }
            }
            // Wave 10: boss + 3 minibosses + gewone enemies
            else if (waveNumber == 10)
            {
                SpawnBoss();
                for (int i = 0; i < 3; i++)
                {
                    SpawnMiniBoss();
                }
                yield return SpawnRegularEnemies();
            }
            // Elke 5e wave (5, 15): 1 miniboss + gewone enemies
            else if (waveNumber % 5 == 0)
            {
                SpawnMiniBoss();
                yield return SpawnRegularEnemies();
            }
            // Alle andere waves: alleen gewone enemies
            else
            {
                yield return SpawnRegularEnemies();
            }

            yield return new WaitUntil(() => enemiesAlive <= 0);
            waveNumber++;
            yield return new WaitForSeconds(waveDelay);
        }

        Debug.Log("Alle waves voltooid!");
    }

    private IEnumerator SpawnRegularEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnEnemy()
    {
        if (WayPoints.waypoints == null || WayPoints.waypoints.Length == 0)
        {
            Debug.LogError("Geen waypoints gevonden!");
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

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, WayPoints.waypoints[0].position, Quaternion.identity);
        boss.name = "Boss Clone";
        enemiesAlive++;
        boss.GetComponent<EnemyHP>().spawner = this;
    }

    private GameObject SelectEnemy()
    {
        if (waveNumber < 3)
        {
            return enemyPrefabs[0];
        }
        else
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public int GetMaxWaves()
    {
        return maxWaves;
    }
}