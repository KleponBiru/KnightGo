using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public Scene scene;
    public string sceneName;

    public class SceneData
    {
        public string level;
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene(); // scene aktif
        sceneName = scene.name; // nama scene aktif
    }

    public void SaveData()
    {
        SceneData data = new SceneData();
        data.level = sceneName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath+"/SaveFile.json", json);
        Debug.Log(json);
    }

    public void LoadData()
    {
        string loadJson = File.ReadAllText(Application.dataPath+"/SaveFile.json");
        SceneData loadData = JsonUtility.FromJson<SceneData>(loadJson);

        sceneName = loadData.level;
    }
}