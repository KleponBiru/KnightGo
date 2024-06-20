using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    DefeatComing defeatComing;
    public PlayerMovement playerMovement;

    public bool win = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // kalo tag Player lewatin trigger menang
        {
            // Debug.Log("MENANGGG !!!!");
            win = true; //set win = true yg ngatur DefeatComing.WinCheck(), 
        }
    }
}
