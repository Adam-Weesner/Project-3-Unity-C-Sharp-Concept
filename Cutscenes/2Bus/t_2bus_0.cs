using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_2bus_0 : MonoBehaviour {

    public GameObject player;
    public GameObject cutsceneScript;

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

            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        // Drops player to floor
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Rigidbody2D>().gravityScale = 7;
        yield return new WaitForSeconds(0.1f);

        cutsceneScript.GetComponent<c_2bus_2>().StartCutscene();
    }
}