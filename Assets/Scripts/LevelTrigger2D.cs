using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger2D : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";  // Tag for player object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}");  // Log the collision

        if (collision.CompareTag(playerTag))  // Check if the object is the player
        {
            Debug.Log("Player has entered the trigger zone.");

            // Get the current level index
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            Debug.Log($"Current Level: {currentLevel}");

            // Determine the next level
            int nextLevel = currentLevel + 1;

            // Check if the next level exists
            if (nextLevel < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log($"Loading Next Level: {nextLevel}");
                SceneManager.LoadScene(nextLevel);  // Load the next level
            }
            else
            {
                Debug.Log("No more levels to load.");
            }
        }
    }
}
