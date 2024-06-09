using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DefeatComing : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector3 direction = Vector3.right;

    public bool canGoRight = false;
    public WinTrigger winTrigger;
    public PlayerMovement playerMovement;
    
    public void Update()
    {
        GoRight();
        WinCheck();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.canMove = false;
            playerMovement.dead = true;
        }
        else if(other.gameObject.CompareTag("DestroyableGround"))
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("DestroyableWall"))
        {
            Destroy(other.gameObject);
        }
    }

    void GoRight()
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

    void WinCheck()
    {
        if(winTrigger.win == true)
        {
            canGoRight = false;
        }
    }
}
