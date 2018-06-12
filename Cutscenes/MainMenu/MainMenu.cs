using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fadeOut;
    public AudioSource fadedMusic;

    public void Continue()
    {
        // Get choices
        Test_Choices.choseA = GetBool("choseA");

        // Fade out
        StartCoroutine(FadeContinue());
    }

    public void NewGame()
    {
        StartCoroutine(FadeNew());
    }

    bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
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

    // Fade out continue
    IEnumerator FadeContinue()
    {
        StartCoroutine(FadeAudio(fadedMusic, 2.5f));
        float fadeTime = fadeOut.GetComponent<Cutscene_Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime+3);

        SceneManager.LoadScene(PlayerPrefs.GetInt("scene", 1));
    }

    // Fade out new game
    IEnumerator FadeNew()
    {
        StartCoroutine(FadeAudio(fadedMusic, 2.5f));
        float fadeTime = fadeOut.GetComponent<Cutscene_Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime+3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}