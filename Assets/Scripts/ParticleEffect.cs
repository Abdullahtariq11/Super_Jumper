using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [Header("Hurt Blood")]

    [SerializeField] private Health objectHealth;
    private float tempHealth;
    private float previousHealth;
    public ParticleSystem HurtEffect ;
    void Awake()
    {
        objectHealth = GetComponent<Health>();
        //HurtEffect = GetComponent<ParticleSystem>();
        
    }

    private void Start()
    {
        previousHealth = objectHealth.current_health;
    }

    void Update()
    {
        tempHealth = objectHealth.current_health;
        isHurt();
    }

    private void isHurt()
    {
        
        if (tempHealth < previousHealth)
        {
        
            HurtEffect.Play();
            previousHealth=tempHealth ;
            StartCoroutine(isHurtDelay());
    
        }
      
    }

    private IEnumerator isHurtDelay()
    {
        yield return new WaitForSeconds(1f);
        HurtEffect.Stop();
    }

}
