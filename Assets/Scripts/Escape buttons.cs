using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas; 
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (pauseCanvas != null) 
        {
            if (isPaused)
            {
                ResumeGame(); 
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseCanvas.SetActive(true); 
        Time.timeScale = 0f; 
        AudioListener.pause = true; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false); 
        Time.timeScale = 1f; 
        AudioListener.pause = false; 
    }
}
