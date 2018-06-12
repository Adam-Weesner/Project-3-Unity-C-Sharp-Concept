using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_0intro_1 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject rainObject;
    public GameObject busObject;
    public GameObject letterbox;
    public GameObject introCanvas;
    public GameObject introFolder;
    public GameObject busFolder;
    public Texture2D fadeWhite;
    public Texture2D fadeBlack;
    public AudioSource rain;

    // Starts opening cutscene
    void Start()
    {
        // Temporarily disables the player movement during opening cutscene
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Animator>().SetBool("IsRunning", false);
        player.GetComponent<Rigidbody2D>().isKinematic = true;

        StartCoroutine(Cutscene0());
    }

    IEnumerator Cutscene0()
    {
        // Changes fade in to white
        gameObject.GetComponent<Cutscene_Fade>().fadeTexture = fadeWhite;

        // Disable camera tracking
        camObj.GetComponent<CameraSystem>().playerActive = false;

        // Play rain sound
        rainObject.SetActive(true);
        rain.Play();

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Fade in
        GetComponent<Cutscene_Fade>().fadeSpeed = 0.4f;
        GetComponent<Cutscene_Fade>().enabled = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(Fade(-1));

        // BUS WITH LIGHTNING --------------------------------------------------------
        // Intro camera sweep
        camObj.GetComponent<Animator>().Play("c_0intro_1"); 

        yield return new WaitForSeconds(0.7f);

        busObject.GetComponent<Animator>().Play("b_0intro_1");

        yield return new WaitForSeconds(12);

        // Fade out
        gameObject.GetComponent<Cutscene_Fade>().fadeTexture = fadeBlack;
        GetComponent<Cutscene_Fade>().fadeSpeed = 0.4f;
        GetComponent<Cutscene_Fade>().enabled = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(Fade(1));

        yield return new WaitForSeconds(1);

        // Disable background
        introCanvas.SetActive(false);
        gameObject.GetComponent<Cutscene_Fade>().enabled = false;

        // Disable rain
        rainObject.SetActive(false);
        rain.Stop();

        yield return new WaitForSeconds(3);

        // Create opening dialogue
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);
        player.GetComponent<Dialogue_Start>().StartConversation(4);
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue4").GetComponent<Dialogue_Manager>().completed);

        yield return new WaitForSeconds(2);

        // Enabling/deactivating folders
        rainObject.SetActive(false);
        introFolder.SetActive(false);
        busFolder.SetActive(true);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }
}
