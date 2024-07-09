using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using System.IO;

public class MainMenu : MonoBehaviour
{
    public SaveManager saveManager;

    public Canvas canvas;

    public GameObject panelStart;
    public Button runButton;
    public Button continueVButton;
    public Button surrendButton;

    public class SceneData
    {
        public string level;
    }
    
    void Start()
    {
        runButton.onClick.AddListener(RunButtonClicked);
        continueVButton.enabled = false;
        
        string loadJson = File.ReadAllText(Application.dataPath+"/SaveFile.json");
        SceneData loadData = JsonUtility.FromJson<SceneData>(loadJson);
        if(loadData.level == "Game")
        {
            continueVButton.enabled = true;
            continueVButton.onClick.AddListener(ContinueButtonClicked);
        }
        surrendButton.onClick.AddListener(SurrendButtonClicked);
    }

    public void RunButtonClicked()
    {
        SceneManager.LoadScene("game");
    }

    public void ContinueButtonClicked()
    {
        SceneManager.LoadScene("Game2");
    }

    public void SurrendButtonClicked()
    {
        Application.Quit();
    }
}
