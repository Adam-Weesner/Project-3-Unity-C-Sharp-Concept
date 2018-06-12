using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_0Intro_Intro : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject landscape;
    public GameObject rainObject;
    public GameObject letterbox;
    public GameObject introFolder;
    public GameObject dreamFolder;
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
        // Disable camera tracking
        camObj.GetComponent<CameraSystem>().playerActive = false;

        //Play rain sound
        rain.Play();

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Fade in, then disable fade
        GetComponent<Cutscene_Fade>().fadeSpeed = 0.4f;
        GetComponent<Cutscene_Fade>().enabled = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(Fade(-1));
        yield return new WaitForSeconds(3);

        // Turn off landscape
        landscape.SetActive(false);

        yield return new WaitForSeconds(1);

        // INTRO DIALOGUE

        // Create opening dialogue
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);
        player.GetComponent<Dialogue_Start>().StartConversation(0);
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue0").GetComponent<Dialogue_Manager>().completed);

        yield return new WaitForSeconds(1.5f);

        // 2nd dialogue
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);
        player.GetComponent<Dialogue_Start>().StartConversation(1);
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue1").GetComponent<Dialogue_Manager>().completed);

        yield return new WaitForSeconds(1);


        // BUS WITH LIGHTNING --------------------------------------------------------
        // Intro camera sweep
        yield return new WaitForSeconds(1);

        // Fade in
        GetComponent<Cutscene_Fade>().fadeSpeed = 2;
        StartCoroutine(Fade(-1));

        camObj.GetComponent<Animator>().Play("c_intro_bus");

        // Turn on landscape
        landscape.SetActive(true);

        // Play thunder sound


        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length-1);

        // Fade out
        StartCoroutine(Fade(1));
        yield return new WaitForSeconds(1);
        GetComponent<Cutscene_Fade>().enabled = false;

        // Turn off landscape
        landscape.SetActive(false);

        // 3rd dialogue
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);
        player.GetComponent<Dialogue_Start>().StartConversation(2);
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue2").GetComponent<Dialogue_Manager>().completed);

        yield return new WaitForSeconds(1);


        // 2ND BUS CUTSCENE ------------------------------------------------------------
        // Intro camera sweep
        yield return new WaitForSeconds(1);

        // Fade in
        GetComponent<Cutscene_Fade>().fadeSpeed = 0.4f;
        GetComponent<Cutscene_Fade>().enabled = true;
        StartCoroutine(Fade(-1));

        camObj.GetComponent<Animator>().Play("c_0_bus");

        // Turn on landscape
        landscape.SetActive(true);

        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+4);

        // Fade out
        StartCoroutine(Fade(1));
        yield return new WaitForSeconds(1);
        GetComponent<Cutscene_Fade>().enabled = false;

        // Turn off landscape
        landscape.SetActive(false);

        // 3rd dialogue
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);
        player.GetComponent<Dialogue_Start>().StartConversation(3);
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue3").GetComponent<Dialogue_Manager>().completed);

        yield return new WaitForSeconds(1.5f);

        // Fade out
        GetComponent<Cutscene_Fade>().enabled = true;
        StartCoroutine(Fade(1));

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");
        yield return new WaitForSeconds(1);

        // Stop rain sound
        rain.Stop();
        rainObject.SetActive(false);

        // Enabling/deactivating folders
        rainObject.SetActive(false);
        introFolder.SetActive(false);
        dreamFolder.SetActive(true);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }
}
