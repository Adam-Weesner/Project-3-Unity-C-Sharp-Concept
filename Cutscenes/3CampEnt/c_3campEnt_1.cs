using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_3campEnt_1 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;

    // Use this for initialization
    public void StartCutscene()
    {
        StartCoroutine(Cutscene0());
    }

    IEnumerator Cutscene0()
    {
        yield return new WaitForSeconds(0.1f);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        yield return new WaitForSeconds(0.1f);

        // Camera animation
        camObj.GetComponent<Animator>().Play("c_3campEnt_0");

        // Wait for end of animation
        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
