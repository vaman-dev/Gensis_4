using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Debug.Log("💥 Player hit by EnemyBullet!");
            ReloadScene(); // Reload the current scene
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
