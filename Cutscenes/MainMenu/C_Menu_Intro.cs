using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Menu_Intro : MonoBehaviour
{
    public GameObject logo;
    public GameObject menu;

    // Starts opening cutscene
    void Start()
    {
        StartCoroutine(Cutscene0());
    }

    IEnumerator Cutscene0()
    {
        yield return new WaitForSeconds(1);

        // Fade in logo
        float fadeTime = GetComponent<Cutscene_Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime+3);
        
        // Fade out logo
        fadeTime = GetComponent<Cutscene_Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime+3);

        // Disable logo and enable menu
        logo.SetActive(false);
        menu.SetActive(true);

        // Fade in menu
        fadeTime = GetComponent<Cutscene_Fade>().BeginFade(-1);
        yield return new WaitForSeconds(fadeTime+3);

    }
}