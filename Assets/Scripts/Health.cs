using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float starting_health;
    public float current_health { get; private set; }
    public float counterImpact { get; private set; }


    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer mySprite;
    [SerializeField] private Animator myAnim;
    private bool isDead;
    //public GameObject blood;
    

    void Awake()
    {
        current_health = starting_health;
        counterImpact = 0;
        mySprite =GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        isDead = false;
    }




    public void take_damage(float _damage)
    {
        current_health = Mathf.Clamp(current_health - _damage, 0, starting_health);
     /*   if (this.gameObject.tag == "Player" && current_health > 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
        }*/

        if (current_health > 0)
        {
            counterImpact++;
            StartCoroutine(Invunerability());
        }
        else if(!isDead)
        {
            counterImpact++;
            isDead = true;
            myAnim.SetTrigger("isDie");
            StartCoroutine(beforeDead());

        }
    }

    public void increaseHealth()
    {
        if (current_health < starting_health)
        {
            current_health++;
        }
    }


   private IEnumerator Invunerability()
    {
        
        for (int i = 0; i < numOfFlashes; i++)
        {
            mySprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            mySprite.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
     
    }

   private IEnumerator beforeDead()
    {
        
         yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

    }


}
