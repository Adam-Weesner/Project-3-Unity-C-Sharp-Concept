using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    public GameObject player;

    // Clamps the camera into position
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public bool playerActive = false;  // Bool to activate camera following player
    public GameObject character;

    // Use this for initialization
    void Start () {
        if (character != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}

    // Called at end of update cycle
    void LateUpdate() {
        if (playerActive && character != null) {
            // Reads the position of the player
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

            // Sets of position (in vector form of x,y,z) of this object (the camera) based on the player's position (x & y) while keeping z the same
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
    }
}
