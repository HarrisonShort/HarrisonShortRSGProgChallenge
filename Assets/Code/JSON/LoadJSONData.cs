using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class that loads data from JSON file, and then 
/// stores that data to be used 
/// </summary>
public class LoadJSONData : MonoBehaviour
{
    private string path;
    private string jsonString;

    private EnemyWaveController enemyWaveController;

    void Awake ()
    {
        path = Application.streamingAssetsPath + "/Wave.json";
        jsonString = File.ReadAllText(path);

        WaveData testWave = JsonUtility.FromJson<WaveData>(jsonString);

        // Find the component for EnemyWaveController and push JSON data to it
        enemyWaveController = GetComponent<EnemyWaveController>();

        if (enemyWaveController != null)
        {
            enemyWaveController.GetJSONData(testWave.Enemies, testWave.Waves);
        }
        else
        {
            Debug.LogWarning("Warning: There is no EnemyWaveController component on " + 
                             "this object. Please make sure one is attached.");
        }
    }
}
