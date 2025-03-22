using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coinCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinText();
            Destroy(collision.gameObject);
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = ":" + coinCount;
    }
}