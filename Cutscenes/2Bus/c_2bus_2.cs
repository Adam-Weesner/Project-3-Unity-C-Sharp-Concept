using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_2bus_2 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public GameObject busFolder;
    public GameObject campEntranceFolder;


    // Use this for initialization
    public void StartCutscene()
    {
        player.GetComponent<Transform>().position = new Vector3(-693.58f, -95.68394f, 0);
        camObj.GetComponent<CameraSystem>().character = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        StartCoroutine(Cutscene0());
	}

    IEnumerator Cutscene0()
    {
        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // Flips player
        player.GetComponent<SpriteRenderer>().flipX = true;

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        yield return new WaitForSeconds(0.1f);

        // Fade out
        gameObject.GetComponent<Cutscene_Fade>().enabled = true;
        StartCoroutine(Fade(1));

        yield return new WaitForSeconds(3);

        // Transition to next scene ------------------------------------------------------
        // Enabling/deactivating folders
        busFolder.SetActive(false);
        campEntranceFolder.SetActive(true);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }
}
