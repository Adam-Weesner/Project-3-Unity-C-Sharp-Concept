using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_1dream_1 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public GameObject thomas;
    public AudioSource newMusic;

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

        // Plays music
        newMusic.Play();

        // Intro camera sweep
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().Play("c_dream_1");
        yield return new WaitForSeconds(3);

        // Play Thomas animation
        thomas.GetComponent<Animator>().Play("t_dream_1");

        // Wait for end of animation
        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 2);

        // Sets camera to focus on player
        camObj.GetComponent<CameraSystem>().character = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        // Sets dialogue box to move onscreen
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);

        // Create dialogue
        player.GetComponent<Dialogue_Start>().StartConversation(2);

        // Waits until dialogue is finished
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue2").GetComponent<Dialogue_Manager>().completed);
        yield return new WaitForSeconds(0.1f);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");

        // Removes trigger
        Destroy(gameObject);
    }
}
