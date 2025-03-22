using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableLevelTrigger : MonoBehaviour
{
    private int previousSceneIndex;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("DisableLevelTrigger script is active.");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("DisableLevelTrigger script has been destroyed.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == previousSceneIndex)
        {
            LevelTrigger levelTrigger = FindObjectOfType<LevelTrigger>();
            if (levelTrigger != null)
            {
                levelTrigger.gameObject.SetActive(false); // Disable the LevelTrigger object
                Debug.Log("LevelTrigger object has been disabled after returning to the previous scene.");
            }
        }
    }
}
