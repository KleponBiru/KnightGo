using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public Animator animator;

    private Collider2D collider;

    public float speed = 0f;
    public bool isDeath = false;

    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(playerMovement.canDash == false)
            {
                animator.SetFloat("Speed", 0);
                animator.SetBool("isDeath", true);
                collider.isTrigger = true; //biar bisa dilewatin

                FadeOut();
                Invoke("DestroySelf", 2f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            FadeOut();
            Invoke("DestroySelf", 1f);
        }
    }

    void FadeOut()
    {
        animator.SetBool("FadeOut", true);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}