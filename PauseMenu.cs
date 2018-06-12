using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseCanvas;
    public GameObject fadedMusic;

    // If paused, then show 
    void Update()
    {
        if (isPaused)
        {
            // Will pause time in game
            Time.timeScale = 0.0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            // Will resume time in game
            Time.timeScale = 1.0f;
            pauseCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    int SetBoolToInt(bool value)
    {
        if (value)
        {
            return 1;
        }
        return 0;
    }

    // Will resume the game
    public void Resume()
    {
        isPaused = false;
    }

    // Will Save your major choices and the current scene index.
    public void SaveExit()
    {
        // Fade out music
        //StartCoroutine(FadeAudio(fadedMusic, 2.5f));
        //allAudio

        // Will save the scene index
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);

        // Saves the choices
        PlayerPrefs.SetInt("choseA", SetBoolToInt(Test_Choices.choseA));

        Time.timeScale = 1.0f;

        Application.Quit();
        //SceneManager.LoadScene(0);
    }

    // Fade audio
    IEnumerator FadeAudio(AudioSource fadeMusic, float FadeTime)
    {
        float startVolume = fadeMusic.volume;

        while (fadeMusic.volume > 0)
        {
            fadeMusic.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        fadeMusic.Stop();
        fadeMusic.volume = startVolume;
    }

    /*IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        
        // Load main menu
        SceneManager.LoadScene(0);
    }*/
}