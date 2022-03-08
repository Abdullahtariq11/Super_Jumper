using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    private float coinsCollected = 0;

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText.text = 'x' + coinsCollected.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            coinsCollected++;
            coinText.text = 'x' + coinsCollected.ToString();
            Destroy(collision.gameObject);
        }

    }

}
