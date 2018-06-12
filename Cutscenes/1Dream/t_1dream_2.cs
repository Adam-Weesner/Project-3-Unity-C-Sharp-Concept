using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_1dream_2 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public GameObject thomas;
    public GameObject audioFolder;
    public GameObject introFolder;
    public GameObject dreamFolder;
    public GameObject introLandscape;

    private bool first = true;

    // When player enters a spawn point A, set player's position to spawn point b
    void OnTriggerStay2D(Collider2D trig)
    {
        if (first)
        {
            // Temporarily disables the player movement during opening cutscene
            player.GetComponent<Rigidbody2D>().gravityScale = 100;
            player.GetComponent<Player_Move>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<Animator>().SetBool("IsRunning", false);

            first = false;
            StartCoroutine(Cutscene0());
        }
    }

    IEnumerator Cutscene0()
    {
        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Drops player to floor
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Rigidbody2D>().gravityScale = 7;
        
        // Intro camera sweep
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().Play("c_dream_2");
        yield return new WaitForSeconds(3);

        // Wait for end of animation
        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 2);

        // Fade out
        gameObject.GetComponent<Cutscene_Fade>().enabled = true;
        StartCoroutine(Fade(1));

        yield return new WaitForSeconds(3);

        //Transition to next scene
        // Stop music 
        audioFolder.SetActive(false);

        // Enabling/deactivating folders
        dreamFolder.SetActive(false);
        introFolder.SetActive(true);
        introLandscape.SetActive(true);

        introFolder.GetComponentInChildren<C_0Intro_Intro>().enabled = false;
        introFolder.GetComponentInChildren<c_0intro_1>().enabled = true;
    }

    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }
}
