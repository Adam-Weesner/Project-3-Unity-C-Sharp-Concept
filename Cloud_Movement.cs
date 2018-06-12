using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Movement : MonoBehaviour {

    public float speed;
    private float xDir;
    public float minDis = -15;
    public float reset = 30;

	// Use this for initialization
	void Start () {
        xDir = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        xDir -= Time.deltaTime * speed;
        transform.position = new Vector3(xDir, transform.position.y, transform.position.z);

        // If the object reaches too much to the left, then reset position to right
        if (transform.position.x < minDis)
        {
            transform.position = new Vector3(reset, transform.position.y, transform.position.z);
            xDir = transform.position.x;
        }
	}
}
