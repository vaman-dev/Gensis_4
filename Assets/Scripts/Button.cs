using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;

public class Button : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;  
    [SerializeField] private GameObject currentCanvas;
    [SerializeField] private GameObject videoCanvas;
    [SerializeField] private float delayTime = 2f;
    [SerializeField] private AudioSource thunderAudio;
    [SerializeField] private AudioSource startingAudio;

    private void Start()
    {
        if (videoCanvas != null) videoCanvas.SetActive(false);
         if (startingAudio != null) startingAudio.Play();
    }

    public void OnClick()
    {
        Debug.Log("Button Clicked");

        if (currentCanvas != null) currentCanvas.SetActive(false);
        if (videoCanvas != null) videoCanvas.SetActive(true);
        if (videoPlayer != null && !videoPlayer.isPlaying) videoPlayer.Play();
        if (thunderAudio != null) thunderAudio.Play();

        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        
            Application.Quit(); // Quit for built game
    
    }

    public void Options()
    {
        Debug.Log("Options");
        // Add options handling logic here
    }
}
