using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void RestartCurrentLevel()
    {
        // Get the current active scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Restart the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}