using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void RestartCurrentLevel()
    {
        // Reset time scale in case it was paused
        Time.timeScale = 1f;

        // Get the current active scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Restart the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}