using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{

    
    public float current_stamina { get; private set; }
    public float starting_stamina  { get; private set; }
    [SerializeField] private float stamina_Chargespeed;
    [SerializeField] private float stamina_Dischargespeed;
    private Animator myAnim;





    void Awake()
    {
        starting_stamina = 10;
        current_stamina = starting_stamina;
        myAnim = GetComponent<Animator>();

    }


    void Update()
    {
        


    }


    public void staminaChange(bool change)
    {
        
        if(change==true)
        {
            Discharge();
        }

        else if (change == false )
        {
            StartCoroutine(Charge());
           
        }
    }

    private IEnumerator Charge()
    {
        yield return new WaitForSeconds(2);
        if (current_stamina < starting_stamina)
        {

            current_stamina = current_stamina + stamina_Chargespeed * Time.deltaTime;
           

        }
        else
        {
            current_stamina = starting_stamina;
        }
    }

    private void Discharge()
    {
        if (current_stamina > 0f)
        {
            current_stamina = current_stamina - stamina_Dischargespeed * Time.deltaTime;
            
        }
        else
        {
            current_stamina = 0f;
        }
    }
}
