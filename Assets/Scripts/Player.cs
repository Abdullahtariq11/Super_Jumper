using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Player_body;
    private CapsuleCollider2D Player_feets;
    private Animator Player_anim;
    private Health player_health;

    [SerializeField] float Speed;
    [SerializeField] float JumpForce;
    private float direction;
    private bool isGrounded;

 

    void Awake()
    {
        Player_body = GetComponent<Rigidbody2D>();
        Player_feets = GetComponent<CapsuleCollider2D>();
        Player_anim = GetComponent<Animator>();
        player_health = GetComponent<Health>();
    }

   
    void Update()
    {
        Run();  
        Jump();

    }



    private void Run()
    {
        direction= Input.GetAxis("Horizontal");

        if(Mathf.Abs(direction)>0)
        {
            if (Mathf.Sign(direction) > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (Mathf.Sign(direction) < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            Player_body.velocity = new Vector2(direction * Speed  , Player_body.velocity.y);
            Player_anim.SetBool("isRun", true);
        }

        else
        {
            Player_anim.SetBool("isRun", false);
        }

        
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Player_body.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            Player_anim.SetTrigger("isJump");
            isGrounded = false;
        }
            
    }

    private void Die()
    {
        Player_anim.SetTrigger("isDead");
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            
        }
        else if (collision.gameObject.tag == "Trap")
        {
            player_health.take_damage(1);
            isGrounded = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            player_health.take_damage(1);

        }
    }



}
