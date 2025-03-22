using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Debug.Log("Bullet hit");
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    }

    public void RestartGame()
    {
        AudioListener.pause = false; // Unpause sound when restarting
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            Debug.Log("Player collided with Ball");
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    }
}
