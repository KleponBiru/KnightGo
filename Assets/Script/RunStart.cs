using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using System.IO;

public class RunStart : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public WinTrigger winTrigger;
    public SaveManager saveManager;

    public Canvas canvas;

    public GameObject panelStart;
    public Button runButton;
    public Button continueVButton;
    public Button surrendButton;

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
        //biar keliatan Canvas sama panelStart doank
        panelWin.SetActive(false); //matiin panelWin
        panelDefeat.SetActive(false); //matiin panelDefeat
        panelSkill.SetActive(false); //matiin panelSkill
        
        runButton.onClick.AddListener(RunButtonClicked); //panggil RunButtonClicked
        continueVButton.enabled = false;
        
        string loadJson = File.ReadAllText(Application.dataPath+"/SaveFile.json");
        SceneData loadData = JsonUtility.FromJson<SceneData>(loadJson);
        if(loadData.level == "game1")
        {
            continueVButton.enabled = true;
            continueVButton.onClick.AddListener(ContinueButtonClicked);
        }
        surrendButton.onClick.AddListener(SurrendButtonClicked); //panggil SurrendButtonClicked
    }

    public void Update()
    {
        if(winTrigger.win == true)
        {
            //kalo win munculin panelWin
            // Debug.Log("menang woi ini harusnya muncul restartButton");
            canvas.enabled = true;
            panelWin.SetActive(true);
            panelSkill.SetActive(false);
            
            nextButton.onClick.AddListener(NextButtonClicked);
            winRestartButton.onClick.AddListener(RestartButtonClicked);
            exitMenuButton.onClick.AddListener(ExitButtonClicked);
            playerMovement.canMove = false; //berentiin player

            saveManager.SaveData();
        }

        if(playerMovement.dead == true)
        {
            //kalo kalah munculin panelDefeat
            // Debug.Log("ini kalah harusnya muncul restart jg");
            canvas.enabled = true;
            panelDefeat.SetActive(true);
            panelSkill.SetActive(false);

            exitButton.onClick.AddListener(ExitButtonClicked);
            defeatRestartButton.onClick.AddListener(RestartButtonClicked);
        }
    }

    public void RunButtonClicked()
    {
        //mulai game = semua gerak
        // Debug.Log("Run Clicked");
        playerMovement.canMove  = true; //player bisa gerak
        defeatComing.canGoRight = true; //mulai ngejar
        panelSkill.SetActive(true);
        panelStart.SetActive(false); //matiin panelStart biar ga muncul kalo kalah / menang
    }

    public void ContinueButtonClicked()
    {
        SceneManager.LoadScene("Game2");
    }

    public void SurrendButtonClicked()
    {
        // Debug.Log("Exit clicked");
        Application.Quit();
    }

    public void NextButtonClicked()
    {
        // Debug.Log("NextButton clicked");
        SceneManager.LoadScene("Game2");
    }

    public void RestartButtonClicked()
    {
        //ulang level
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ExitButtonClicked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        
        canvas.enabled = true;
        panelDefeat.SetActive(false);
        panelWin.SetActive(false);
        Invoke("delay", 5f);
    }

    public void delay()
    {
        panelStart.SetActive(true);
    }
}