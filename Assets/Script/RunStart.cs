using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunStart : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public WinTrigger winTrigger;

    public Canvas canvas;

    public GameObject panelStart;
    public Button runButton;
    public Button surrendButton;

    public GameObject panelWin;
    public Button nextButton;
    public Button winRestartButton;

    public GameObject panelDefeat;
    public Button loseRestartButton;

    public GameObject panelSkill;

    void Start()
    {
        //biar keliatan Canvas sama panelStart doank
        panelWin.SetActive(false); //matiin panelWin
        panelDefeat.SetActive(false); //matiin panelDefeat
        panelSkill.SetActive(false); //matiin panelSkill
        
        runButton.onClick.AddListener(RunButtonClicked); //panggil RunButtonClicked
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
            playerMovement.canMove = false; //berentiin player
        }

        if(playerMovement.dead == true)
        {
            //kalo kalah munculin panelDefeat
            // Debug.Log("ini kalah harusnya muncul restart jg");
            canvas.enabled = true;
            panelDefeat.SetActive(true);
            panelSkill.SetActive(false);
            loseRestartButton.onClick.AddListener(RestartButtonClicked);
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

    public void SurrendButtonClicked()
    {
        // Debug.Log("Exit clicked");
        Application.Quit();
    }

    public void NextButtonClicked()
    {
        // Debug.Log("NextButton clicked");
    }

    public void RestartButtonClicked()
    {
        //ulang level
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}