using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableObjectOnTrigger : MonoBehaviour
{
    public GameObject objectToDisableOnReturn;
    public MonoBehaviour scriptToActivateOnReturn;

    private int previousSceneIndex;

    private void Start()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered the object.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == previousSceneIndex)
        {
            if (objectToDisableOnReturn != null)
            {
                objectToDisableOnReturn.SetActive(false);
                Debug.Log("Object disabled on returning to the previous scene.");
            }

            if (scriptToActivateOnReturn != null)
            {
                scriptToActivateOnReturn.enabled = true;
                Debug.Log("Script activated on returning to the previous scene.");
            }
        }
    }
}
