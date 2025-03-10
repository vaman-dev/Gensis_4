using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    [SerializeField] public GameObject EndCanvas;  
    [SerializeField] public AudioSource EndAudio; 

    void Start() 
    {
        
        if (EndAudio != null)
        {
            EndAudio.Stop();
        }

        if (EndCanvas != null)
        {
            EndCanvas.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("YOU reached the point");

         
        if (EndCanvas != null)
        {
            EndCanvas.SetActive(true);
        }

        if (EndAudio != null)
        {
            EndAudio.Play();
        }

        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Works only in a built application
    }
}
