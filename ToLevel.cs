using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel : MonoBehaviour {

    public int index = 0;

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D trig)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}
