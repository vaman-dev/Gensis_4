using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            Debug.Log("Player has entered the trigger zone."); // Log entry confirmation

            int currentLevel = SceneManager.GetActiveScene().buildIndex; // Get current level
            Debug.Log("Current Level Index: " + currentLevel); // Log current level

            int nextLevel = currentLevel + 1; // Increase level by 1

            if (nextLevel < SceneManager.sceneCountInBuildSettings) // Ensure the next level exists
            {
                SceneManager.LoadScene(nextLevel); // Load the next level
                Debug.Log("Jumping to Level: " + nextLevel); // Log level jump
            }
            else
            {
                Debug.Log("No more levels to load.");
            }
        }
    }
}