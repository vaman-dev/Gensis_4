using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveJumping : MonoBehaviour
{
      private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player has entered the trigger
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex; // Get current level
            int nextLevel = currentLevel + 1; // Increase level by 1

            if (nextLevel < SceneManager.sceneCountInBuildSettings) // Ensure the next level exists
            {
                Debug.Log("Loading next level: " + nextLevel); // Log the next level being loaded
                SceneManager.LoadScene(nextLevel); // Load the next level
            }
            else
            {
                Debug.Log("No more levels to load.");
            }
        }
    }
}
