using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    
    
        [SerializeField] private Health playerHealth;
        [SerializeField] private Image totalHeathbar;
        [SerializeField] private Image currentHeathbar;
        void Start()
        {
            totalHeathbar.fillAmount = playerHealth.current_health / 10;
        }


        void Update()
        {
            currentHeathbar.fillAmount = playerHealth.current_health / 10;
        }
    }
