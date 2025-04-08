using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText;
    public EnemySpawner spawner;

    private void Start()
    {
        if (waveText == null)
        {
            waveText = GameObject.Find("WaveText").GetComponent<TMP_Text>();
        }

        if (spawner == null)
        {
            spawner = FindObjectOfType<EnemySpawner>();
        }

        UpdateWaveText();
    }

    private void Update()
    {
        waveText.fontSize = 24;  // Pas dit getal aan naar wens

        UpdateWaveText();
    }

    private void UpdateWaveText()
    {
        if (spawner != null && waveText != null)
        {
            waveText.text = "Wave: " + spawner.GetWaveNumber() + " / " + spawner.GetMaxWaves();
        }
    }
}
