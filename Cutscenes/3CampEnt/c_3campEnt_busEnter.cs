using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_3campEnt_busEnter : MonoBehaviour {

    public GameObject player;

    void Start()
    {
        StartCoroutine(Cutscene0());
    }

    IEnumerator Cutscene0()
    {
        yield return new WaitForSeconds(0.1f);

        GetComponent<Cutscene_Fade>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        // Fade in
        StartCoroutine(Fade(-1));

        yield return new WaitForSeconds(1);

        // Enable player movement
        player.GetComponent<Player_Move>().enabled = true;
    }


    IEnumerator Fade(int index)
    {
        float cutsceneTime = GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(cutsceneTime + 3);
    }
}
