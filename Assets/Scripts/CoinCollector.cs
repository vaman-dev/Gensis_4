using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    private int collectedCoins = 0;

    void Start()
    {
        UpdateCoinUI();
        Debug.Log("Coin counter initialized.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) 
        {
            Debug.Log("Coin collected!");
            collectedCoins++;
            Destroy(other.gameObject); 
            UpdateCoinUI();
        }
    }

    void UpdateCoinUI()
    {
        coinText.text = "Umbrella: " + collectedCoins;
    }
}
