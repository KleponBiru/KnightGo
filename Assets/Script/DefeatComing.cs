using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DefeatComing : MonoBehaviour
{
    public WinTrigger winTrigger;
    public PlayerMovement playerMovement;
    public AdditionalEnemy additionalEnemy;

    public bool canGoRight = false; //dibikin gabisa gerak pas awal
    public float speed;
    public Vector3 direction = Vector3.right;
    
    public void Update()
    {
        GoRight();
        WinCheck();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) //kalo kena Player, Player gabisa gerak + animasi mati
        {
            playerMovement.canMove = false; //gabisa gerak
            playerMovement.dead = true; //animasi mati
        }
        else if(other.gameObject.CompareTag("DestroyableGround")) //kalo kena DestroyableGround ancurin
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("DestroyableWall")) //kalo kena DestroyableWall ancurin
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("AdditionalEnemy"))
        {
            speed += 1f;
        }
    }

    void GoRight() //ngatur gerak atau engga
    {
        if(canGoRight == true)
        {
            // Debug.Log("canGoRight = true cuyyy");
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            // Debug.Log("canGoRight = false cuyy");
        }
    }

    void WinCheck() //check udh menang atau blm buat ngatur lanjut gerak atau engga
    {
        if(winTrigger.win == true)
        {
            canGoRight = false;
        }
    }
}
