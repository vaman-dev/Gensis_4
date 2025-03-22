using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameTimeLimit = 60f; // Time limit in seconds
    private float timeRemaining;
    private bool timerIsRunning = false;
    public TextMeshProUGUI timerText; // Reference to TextMeshProUGUI component

    private void Start()
    {
        StartTimer(); // Start timer when the level begins
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(); // Update UI
            }
            else
            {
                timerIsRunning = false;
                Debug.Log("Time is up! Reverting to previous level.");
                RevertToPreviousLevel();
            }
        }
    }

    private void StartTimer()
    {
        timeRemaining = gameTimeLimit;
        timerIsRunning = true;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Remaining: " + timeRemaining.ToString("F2") + "s";
        }
    }

    private void RevertToPreviousLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int previousLevel = currentLevel - 1;

        if (previousLevel >= 0)
        {
            SceneManager.LoadScene(previousLevel); // Load the previous level
        }
        else
        {
            Debug.Log("No previous levels to revert to.");
        }
    }
}
