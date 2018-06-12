using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_2bus_0 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public bool skip = false;
    

    // Starts opening cutscene
    void Start()
    {
        if (!skip)
            StartCoroutine(Cutscene0());
        else
        {
            StartCoroutine(Fade(-1));
            player.GetComponent<Transform>().position = new Vector3(-693.58f, -95.68394f, 0);
            camObj.GetComponent<CameraSystem>().character = player;
            camObj.GetComponent<CameraSystem>().playerActive = true;
        }
    }


    IEnumerator Cutscene0()
    {
        // Moves player to location
        player.GetComponent<Transform>().position = new Vector3(-693.58f, -95.68394f, 0);

        yield return new WaitForSeconds(0.5f);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        player.GetComponent<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(1);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Intro camera sweep
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().Play("c_2bus_intro");
        yield return new WaitForSeconds(0.1f);

        // Fade in
        StartCoroutine(Fade(-1));

        // Wait for end of animation
        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        // Sets camera to focus on player
        camObj.GetComponent<CameraSystem>().character = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        yield return new WaitForSeconds(0.5f);

        // INTRO ---------------------------------------------------------------------------------------------------
        // Sets dialogue box to move onscreen
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);

        // Create opening dialogue
        player.GetComponent<Dialogue_Start>().StartConversation(0);

        // Waits until dialogue is finished
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue0").GetComponent<Dialogue_Manager>().completed);
        yield return new WaitForSeconds(0.1f);

        // Letterbox disengage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");
        yield return new WaitForSeconds(1);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }
}
