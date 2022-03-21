using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    [Header("coinPickup")]
    private float coinsCollected = 0;
    public TextMeshProUGUI coinText;
    private bool coinCollision;
    private GameObject collectableObject;


    [Header("healthPickup")]
    [SerializeField] private Health playerHealth;
    private bool heartCollision;

    private void Awake()
    {
        heartCollision=false;
        coinCollision =false;
        coinText.text = 'x' + coinsCollected.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin" && !coinCollision)
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("isPickup");
            coinsCollected++;
            coinText.text = 'x' + coinsCollected.ToString();
            coinCollision = true;
            StartCoroutine(beforePickUp());
            collectableObject = collision.gameObject; // had to create new game object with it so that it can be used in another functon 
            
        }

        else if (collision.transform.tag == "Heart" && !heartCollision)
        {
            playerHealth.increaseHealth();
            StartCoroutine(beforePickUp());
            collectableObject = collision.gameObject;
        }

    }

    private IEnumerator beforePickUp()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(collectableObject);
        coinCollision = false;

    }

}
