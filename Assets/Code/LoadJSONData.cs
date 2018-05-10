using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadJSONData : MonoBehaviour
{

    private string path;
    private string jsonString;
    
    

    void Start ()
    {
        path = Application.streamingAssetsPath + "/Wave.json";
        jsonString = File.ReadAllText(path);

        WaveData testWave = JsonUtility.FromJson<WaveData>(jsonString);
    }
}
