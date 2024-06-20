using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = false; //buat bisa jalan atau engga
    public float moveSpeed = 5f;
    public Vector3 moveDirection; 
    public bool isJumping = false; //check lagi jump atau engga
    public float jumpForce = 5f;

    public bool canDash = true;
    public Vector3 dashDirection;
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public float dashTimeLeft;
    public float lastDash = -Mathf.Infinity;
    public float dashCooldown = 2f;

    public Vector3 bounceVelocity;
    public Vector3 currentVelocity;
    public float bounceForce = 2f;

    public Animator animator;
    public bool dead = false; //buat trigger animasi mati
    private bool facingRight = true; //buat trigger bolak balik animasi gerak

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator.SetBool("Defeat", false); //biar ga animasi mati
    }

    void Update()
    {
        if(canMove == true)
        {
            //kalo bisa gerak
            Jump();
            float move = Input.GetAxis("Horizontal");
            moveDirection = new Vector3(move, 0f, 0f);
            // rb2d.velocity = moveDirection*moveSpeed;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            currentVelocity = rb2d.velocity;
            Dash();

            if(move > 0 && !facingRight) //buat ngecheck jalan kanan atau kiri
            {
                Flip(); //buat flip gambar
            }
            else if(move < 0 && facingRight)
            {
                Flip();
            }
            animator.SetFloat("Speed", Mathf.Abs(move)); //buat trigger animasi jalan
        }
        else
        {
            if(dead == true)
            {
                animator.SetBool("Defeat", true); //trigger animasi mati
                animator.SetFloat("Speed", 0); //biar ga animasi jalan
            }
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping) //kalo pencet spasi + ga lagi lompat
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true; //nandain lagi lompat
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(canDash == true){
                dashTimeLeft = dashTime;
                lastDash = Time.time;
                
                if(facingRight)
                {
                    dashDirection = new Vector3(1f,0f,0f);
                }
                else
                {
                    dashDirection = new Vector3(-1f,0f,0f);
                }

                // dashDirection = moveDirection;
                if(dashDirection == Vector3.zero)
                {
                    dashDirection = new Vector3(transform.localScale.x, 0f, 0f);
                }
                rb2d.velocity = dashDirection * dashSpeed;

                if (dashTimeLeft > 0)
                {
                    dashTimeLeft -= Time.deltaTime;
                    if (dashTimeLeft <= 0)
                    {
                        rb2d.velocity = Vector3.zero;
                    }
                }
                canDash = false;
                Invoke("DownTime", 2);
            }
        }
    }

    void DownTime(){
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision) //kalo GameObject yg dikasih script ini collision
    {
        if(collision.gameObject.CompareTag("Ground")) //kalo collision sama tag ground
        {
            isJumping = false; //reset lompat
        }
        else if(collision.gameObject.CompareTag("DestroyableGround")) //sama aja kyk nyentuh tag ground
        {
            isJumping = false;
        }

        if(collision.gameObject.CompareTag("AdditionalEnemy")) //kalo nabrak AdditionalEnemy
        {
            bounceVelocity = new Vector3(-currentVelocity.x, currentVelocity.y, currentVelocity.z);
            rb2d.velocity = Vector3.zero;
            rb2d.AddForce(bounceVelocity * bounceForce, ForceMode2D.Impulse);
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
