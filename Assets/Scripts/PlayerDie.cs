using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathPanel; 
    
 void Start()
{
    Time.timeScale = 1f;
    if (deathPanel != null)
    {
        deathPanel.SetActive(false); 
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Debug.Log("Bullet hit");
            if (deathPanel != null)
            {
                deathPanel.SetActive(true); 
                
                 Time.timeScale = 0f; 
                
                AudioListener.pause = true;
            }
        }
    }

    public void RestartGame()
    {
        AudioListener.pause = false; // Unpause sound when restarting
         Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
