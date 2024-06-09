using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunStart : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public Canvas canvas;

    public Button runButton;
    public Button surrendButton;

    void Start()
    {
        runButton.onClick.AddListener(RunButtonClicked);
        surrendButton.onClick.AddListener(SurrendButtonClicked);
    }

    public void RunButtonClicked()
    {
        // Debug.Log("Run Clicked");
        playerMovement.canMove  = true;
        defeatComing.canGoRight = true;

        canvas.enabled = false;
    }

    public void SurrendButtonClicked()
    {
        // Debug.Log("Exit clicked");
        Application.Quit();
    }
}