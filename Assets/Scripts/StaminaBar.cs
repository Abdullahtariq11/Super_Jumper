using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    
    
        [SerializeField] private Stamina playerStamina;
        [SerializeField] private Image totalHeathbar;
        [SerializeField] private Image currentHeathbar;
        void Start()
        {
            totalHeathbar.fillAmount = playerStamina.current_stamina / 10;
        }


        void Update()
        {
            currentHeathbar.fillAmount = playerStamina.current_stamina / 10;
        }
    }
