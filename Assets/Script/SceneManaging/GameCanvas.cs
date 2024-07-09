using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using System.IO;

public class GameCanvas : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public WinTrigger winTrigger;
    public SaveManager saveManager;

    public Canvas canvas;

    public GameObject panelWin;
    public Button nextButton;
    public Button winRestartButton;
    public Button exitMenuButton;

    public GameObject panelDefeat;
    public Button exitButton;
    public Button defeatRestartButton;

    public GameObject panelSkill;

    public class SceneData
    {
        public string level;
    }

    void Start()
    {
        panelWin.SetActive(false);
        panelDefeat.SetActive(false);
        panelSkill.SetActive(true);

        playerMovement.canMove  = true;
        defeatComing.canGoRight = true;
    }

    void Update()
    {
        if(winTrigger.win == true)
        {
            canvas.enabled = true;
            panelWin.SetActive(true);
            panelSkill.SetActive(false);
            
            nextButton.onClick.AddListener(NextButtonClicked);
            winRestartButton.onClick.AddListener(RestartButtonClicked);
            exitMenuButton.onClick.AddListener(ExitButtonClicked);
            playerMovement.canMove = false;

            // saveManager.SaveData();
            SceneData data = new SceneData();
            Scene scene;
            string sceneName;
            
            scene = SceneManager.GetActiveScene();
            sceneName = scene.name;
            data.level = sceneName;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath+"/SaveFile.json", json);
        }

        if(playerMovement.dead == true)
        {
            canvas.enabled = true;
            panelDefeat.SetActive(true);
            panelSkill.SetActive(false);

            exitButton.onClick.AddListener(ExitButtonClicked);
            defeatRestartButton.onClick.AddListener(RestartButtonClicked);
        }
    }

    public void NextButtonClicked()
    {
        SceneManager.LoadScene("Game2");
    }

    public void RestartButtonClicked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
