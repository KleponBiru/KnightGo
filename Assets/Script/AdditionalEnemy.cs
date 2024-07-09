using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public DefeatComing defeatComing;
    public Animator animator;

    private Collider2D collider;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    public float speed;
    public bool isDeath = false;

    public GameObject enemy;
    public Vector3 enemyPosition;
    public GameObject target;
    public Vector3 targetDestination;
    public float distance;
    public float radius;

    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        targetDestination = target.transform.position;
        enemyPosition = enemy.transform.position;
        distance = Vector3.Distance(enemyPosition, targetDestination);

        if(distance <= radius)
        {
            animator.SetFloat("Speed", 1);
            Invoke("Chase", 0.5f);
        }
        else if(distance > radius)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(playerMovement.canDash == false)
            {
                isDeath = true;
                
                animator.SetFloat("Speed", 0);
                animator.SetBool("isDeath", true);

                rb2d.velocity = new Vector3(0f,0f,0f);
                
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

    public void Chase()
    {
        // enemy.transform.position = distance * Time.deltaTime * speed;
        // enemy.transform.position = Vector3.Lerp(enemyPosition, targetDestination, speed * Time.deltaTime);

        if(rb2d.transform.position.x > target.transform.position.x && isDeath == false)
        {
            speed = 5f;
            rb2d.velocity = Vector3.left * speed;
            sprite.flipX = true;
        }
        else if(rb2d.transform.position.x < target.transform.position.x && isDeath == false)
        {
            speed = 5f;
            rb2d.velocity = Vector3.right * speed;
            sprite.flipX = false;
        }
    }
}