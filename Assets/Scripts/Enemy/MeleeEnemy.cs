using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D m_CapsuleCollider;
    private Animator m_Animator;
    private Health playerHealth;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private Vector3 BoxSize = Vector3.one;

    [SerializeField] private int damage;
    [SerializeField] private LayerMask PlayerLayer;
    private float coolDownTimer;

     private void Awake()
    {
        m_CapsuleCollider=GetComponent<CapsuleCollider2D>();
        m_Animator=GetComponent<Animator>();    


    }

    private void Update()
    {
         
        coolDownTimer += Time.deltaTime;


        //attack when player insight 
        if (playerInSight() && playerHealth.current_health>0)
        {
            if (coolDownTimer > attackCoolDown)
            {
                m_Animator.SetTrigger("isAttack1");
            }
        }

    }

    private bool playerInSight()
    {
        BoxSize = new Vector3(m_CapsuleCollider.bounds.size.x * range, m_CapsuleCollider.bounds.size.y, m_CapsuleCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(m_CapsuleCollider.bounds.center+ transform.right*range*transform.localScale.x* colliderDistance, BoxSize, 0,Vector2.left,0, PlayerLayer);
         if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>(); // as object enemy sees is player so can directly get the health component
        }
        return hit.collider != null; ;
    }

   /* private void OnDrawGizmos()
    {
        BoxSize = new Vector3(m_CapsuleCollider.bounds.size.x * range, m_CapsuleCollider.bounds.size.y, m_CapsuleCollider.bounds.size.z);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(m_CapsuleCollider.bounds.center + transform.right * range*transform.localScale.x*colliderDistance, BoxSize);
    }*/

    private void DamagePlayer()
    {
        if (playerInSight() )
        {   
            playerHealth.take_damage(damage);
        }
    }

}
