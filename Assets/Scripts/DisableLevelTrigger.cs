using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableLevelTrigger : MonoBehaviour
{
    [Header("Scene Management")]
    public int previousSceneIndex; // Viewable in Inspector

    private static DisableLevelTrigger instance; // Singleton instance

    private void Awake()
    {
        // Singleton to prevent duplicate instances
        if (instance != null && instance != this)
        {
            Debug.LogWarning("⚠️ Duplicate DisableLevelTrigger found. Destroying this one.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object between scenes
    }

    private void Start()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"▶️ DisableLevelTrigger is active. Previous Scene Index: {previousSceneIndex}");
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
        Debug.Log("🛑 DisableLevelTrigger has been destroyed.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"📜 Scene Loaded: {scene.name} (Index: {scene.buildIndex})");

        // Check if we have returned to the previous scene
        if (SceneManager.GetActiveScene().buildIndex == previousSceneIndex)
        {
            Debug.Log("🔙 Returned to the previous scene.");

            // Destroy the game object with tag "LevelTrigger"
            GameObject levelTrigger = GameObject.FindGameObjectWithTag("LevelTrigger");
            if (levelTrigger != null)
            {
                Destroy(levelTrigger);
                Debug.Log("🗑️ LevelTrigger object has been destroyed.");
            }
            else
            {
                Debug.LogWarning("⚠️ No LevelTrigger object found.");
            }
        }
        else
        {
            Debug.Log("🟢 Not the previous scene. No action taken.");
        }
    }
}
