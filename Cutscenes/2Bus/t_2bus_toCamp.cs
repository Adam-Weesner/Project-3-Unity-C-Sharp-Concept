using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_2bus_toCamp : MonoBehaviour {

    public GameObject player;
    public GameObject despawn;
    public GameObject spawn;
    public GameObject cutscene;
    public GameObject nextCutscene;
    public GameObject enterSprite;
    public bool isInteractable = true;

    // When player enters a spawn point A, set player's position to spawn point b
    void OnTriggerStay2D(Collider2D trig)
    {
        if (isInteractable)
            enterSprite.SetActive(true);

        if (Input.GetButtonDown("Fire1") && isInteractable)
            StartCoroutine(Teleport(trig));
    }

    void OnTriggerExit2D(Collider2D trig)
    {
        enterSprite.SetActive(false);
    }

    IEnumerator Teleport(Collider2D trig)
    {
        // Disables player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Animator>().SetBool("IsRunning", false);

        cutscene.GetComponent<Cutscene_Fade>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        // Fade out
        cutscene.GetComponent<Cutscene_Fade>().enabled = true;
        StartCoroutine(Fade(1));

        yield return new WaitForSeconds(2);

        // Player transport
        player.GetComponent<Transform>().position = new Vector3(-466.3f, -81.63f, 0);

        yield return new WaitForSeconds(0.1f);

        // Despawns
        despawn.SetActive(false);
        nextCutscene.GetComponent<c_3campEnt_0>().enabled = false;

        // Enables
        spawn.SetActive(true);

        nextCutscene.GetComponent<c_3campEnt_busEnter>().enabled = true;
    }


    IEnumerator Fade(int index)
    {
        float cutsceneTime = cutscene.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(cutsceneTime + 3);
    }
}
