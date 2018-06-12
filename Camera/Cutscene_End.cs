using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_End : MonoBehaviour {

    public float length;

	// Use this for initialization
	void Start () {
        StartCoroutine(JumpToScene());
	}

    IEnumerator JumpToScene()
    {
        // Wait for animation to stop
        yield return new WaitForSeconds(length);

        // Fade out game and load new level
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime+4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
