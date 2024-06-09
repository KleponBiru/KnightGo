using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = false;
    public bool dead = false;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    private bool facingRight = true;
    public Animator animator;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator.SetBool("Lose", false);
    }

    void Update()
    {
        if(canMove == true)
        {
            Jump();
            float move = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(move, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

            if(move > 0 && !facingRight)
            {
                Flip();
            }
            else if(move < 0 && facingRight)
            {
                Flip();
            }
            animator.SetFloat("Speed", Mathf.Abs(move));
        }
        else
        {
            if(dead == true)
            {
                animator.SetBool("Lose", true);
                animator.SetFloat("Speed", 0);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        else if(collision.gameObject.CompareTag("DestroyableGround"))
        {
            isJumping = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
