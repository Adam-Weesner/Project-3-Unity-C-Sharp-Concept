using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Make sure to set "Layer: Ignore Raycast" on the enemy object
public class Enemy_Move : MonoBehaviour {

    public float EnemySpeed;
    public int xMoveDirection;
    public float distance;
	
	// Update is called once per frame
	void Update () {
        // Creates a ray that shoots out 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));

        // Sets the velocity in the x axis multiplied by the speed modifier
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * EnemySpeed;

        // If the distance between the ray and the object is less than 0.7, then flip the direction of the x axis for the object
        if (hit.distance < distance)
        {
            xMoveDirection *= -1;

            // If player collides with enemy, then kill player
            if (hit.collider.tag == "Player")
            {
                SceneManager.LoadScene("Main");
            }
        }
	}
}
