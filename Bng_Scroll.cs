using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bng_Scroll : MonoBehaviour {
    // To have bright background, go to (Quad > name > shader > Unlit/Texture)

    public float speed = 0.5f;  // Speed of scrolling

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per framE
	void Update () {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
