using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{

    public int playerSpeed = 6;   // Sets player speed multiplier
    public int playerJump = 1000;    // Sets player jump power
    public bool isGrounded;   // Is the player on the ground?

    private float moveX;      // Movement on x axis

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlayerJump();

            // Jump animation
            StartCoroutine(Wait());
        }


        // ANIMATION
        // If the player is moving then play running animation
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }


        // PLAYER DIRECTION
        // If player is moving left and facing left, then flip player sprite
        if (moveX < 0.0f && !GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<BoxCollider2D>().offset = new Vector2(0.2f, GetComponent<BoxCollider2D>().offset.y);
        }
        // If player is moving right and facing right, then flip player sprite
        else if (moveX > 0.0f && GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.2f, GetComponent<BoxCollider2D>().offset.y);
        }


        // PHYSICS
        // Moves the player left and right
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void PlayerJump()
    {
        // JUMPING
        // Adds force to up direction
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    IEnumerator Wait()
    {
        GetComponent<Animator>().Play("Player_Jumping");
        yield return new WaitUntil(() => isGrounded);
        GetComponent<Animator>().Play("Player_Idle");

        yield return null;
    }
}
   
