using UnityEngine;
using TMPro;

public class CoinCollector2 : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Reference to the UI Text
    private int coinCount = 0; // Initial coin count

    private void Start()
    {
        UpdateCoinText(); // Initialize the UI text
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) // Check if collided object is a coin
        {
            coinCount++; // Increase coin count
            UpdateCoinText(); // Update the UI

            Destroy(collision.gameObject); // Destroy the coin
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = "" + coinCount.ToString(); // Update the text
    }
}