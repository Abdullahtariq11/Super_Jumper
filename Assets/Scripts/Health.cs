using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float starting_health;
    public float current_health { get; private set; }


    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer mySprite;
    private Animator myAnim;
    

    void Awake()
    {
        current_health = starting_health;
        mySprite=GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
    }


    void Update()
    {
        damage();
       
    }

    public void take_damage(float _damage)
    {
        current_health = Mathf.Clamp(current_health - _damage, 0, starting_health);

        if (current_health > 0)
        {
            
            StartCoroutine(Invunerability());
        }
        else
        {
            myAnim.SetTrigger("isDead");
            StartCoroutine(beforeDead());
           
        }
    }

    private void damage()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            take_damage(1);
        }
    }



    private IEnumerator Invunerability()
    {
        //Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numOfFlashes; i++)
        {
            mySprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            mySprite.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
     //   Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private IEnumerator beforeDead()
    {
         yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

    }


}
