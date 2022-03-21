using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Player_Rigidbody;
    [SerializeField] private CapsuleCollider2D Player_upperBody;
    [SerializeField] private CircleCollider2D Player_feets;
    private Animator Player_anim;
    private Health player_health;
    private Stamina player_stamina;

    [SerializeField] float Speed;
    [SerializeField] float JumpForce;

    private float direction;
    private bool isGrounded;
    private int counter = 0;
    private bool stamina_bool;

    [Header("Melee")]
    private Health enemyHealth;
   // [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask Enemy;
    private Vector3 BoxSize = Vector3.one;
    // private float coolDownTimer;

    [Header("SceneManagement")]
    private string nextSceneName;
    private string CurrentScene;
    private int counterScene;
    private bool isInside=false;
    private bool isThere=false;
    [SerializeField] private Scene_Manager sceneManager;


    void Awake()
    {
        Player_Rigidbody = GetComponent<Rigidbody2D>();
        Player_feets = GetComponent<CircleCollider2D>();
        Player_upperBody= GetComponent<CapsuleCollider2D>();
        Player_anim = GetComponent<Animator>();
        player_health = GetComponent<Health>();
        player_stamina = GetComponent<Stamina>();
        stamina_bool = false;
    }

   
    void Update()
    {
        if (player_health.current_health>0) 
        {
            Run();
            Jump();
            playerTransform();
            Attack();
        }
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

            Player_Rigidbody.velocity = new Vector2(direction * Speed  , Player_Rigidbody.velocity.y);
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
            Player_Rigidbody.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
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

        if (collision.gameObject.tag == "ShedDoorIn" )
        {
            nextSceneName = "ShedInside";
            sceneManager.SceneChanger(nextSceneName);
        }
        else if (collision.gameObject.tag == "ShedDoorOut")
        {
            nextSceneName = "CutScene";
            sceneManager.SceneChanger(nextSceneName);
        }

    }

    private void playerTransform()
    {
       
        if (player_stamina.current_stamina== player_stamina.starting_stamina && Input.GetKeyDown(KeyCode.LeftShift)&&counter==0)
        {

            counter= 1;
            Player_anim.SetLayerWeight(0, 0); //switching off layer 1
            Player_anim.SetLayerWeight(1, 1); //enabling layer 2
            stamina_bool = true;
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) && counter == 1 || player_stamina.current_stamina <= 0)
        {
            counter = 0;
            Player_anim.SetLayerWeight(1, 0);
            Player_anim.SetLayerWeight(0, 1);
            stamina_bool = false;
        }
        player_stamina.staminaChange(stamina_bool);
        //Player_anim.SetLayerWeight(1,0);           //first parameter is layer id means first layer or second layer and second parameter is the weight
    }

    public void Attack()
    {
        if (counter == 1)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Player_anim.SetTrigger("isAttack1");
                damage = 1;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Player_anim.SetTrigger("isAttack2");
                damage = 2;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Player_anim.SetTrigger("isAttack3");
                damage = 4;
            }
        }
    }

    private bool EnemyInSight()
    {
        BoxSize = new Vector3(Player_upperBody.bounds.size.x * range, Player_upperBody.bounds.size.y, Player_upperBody.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(Player_upperBody.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, BoxSize, 0, Vector2.left, 0, Enemy);
        if (hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>(); // as object enemy sees is player so can directly get the health component
        }
        return hit.collider != null; ;
    }

    private void OnDrawGizmos()
    {
        BoxSize = new Vector3(Player_upperBody.bounds.size.x * range, Player_upperBody.bounds.size.y, Player_upperBody.bounds.size.z);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Player_upperBody.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, BoxSize);
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
        {
           
            enemyHealth.take_damage(damage);
        }
    }


}
