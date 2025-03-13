using TMPro;  // 🔹 BELANGRIJK: Zorg dat dit bovenaan staat
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText;  // 🔹 Moet TMP_Text zijn, NIET Text!
    public EnemySpawner spawner;

    private void Start()
    {
        if (waveText == null)
        {
            waveText = GameObject.Find("WaveText").GetComponent<TMP_Text>(); // 🔹 Automatisch zoeken
        }

        if (spawner == null)
        {
            spawner = FindObjectOfType<EnemySpawner>();
        }

        UpdateWaveText();
    }

    private void Update()
    {
        UpdateWaveText();
    }

    private void UpdateWaveText()
    {
        if (spawner != null && waveText != null)
        {
            waveText.text = "Wave: " + spawner.GetWaveNumber();
        }
    }
}
