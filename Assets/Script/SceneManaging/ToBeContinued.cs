using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  
using System.IO;

public class ToBeContinued : MonoBehaviour
{
    public Animator animator;

    public Canvas canvas;
    public GameObject toBeContinued;
    public Button exitButton;

    void Start()
    {
        toBeContinued.SetActive(false);
        Invoke("delay", 1f);
    }

    public void delay()
    {
        toBeContinued.SetActive(true);
        exitButton.onClick.AddListener(ExitButtonClicked);
        
        animator.SetBool("mati", true);
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
