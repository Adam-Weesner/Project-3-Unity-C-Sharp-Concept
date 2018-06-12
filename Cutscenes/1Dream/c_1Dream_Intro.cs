using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_1Dream_Intro : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public AudioSource music;
    public GameObject letterbox;
    public bool skip = false;
    
    private bool active = false;
    private Vector2 direction;
    private float speed;

    // Starts opening cutscene
    void Start()
    {
        if (!skip)
            StartCoroutine(Cutscene0());
        else
        {
            StartCoroutine(Fade(-1));
            player.GetComponent<Transform>().position = new Vector3(-698, -95, 0);
            camObj.GetComponent<CameraSystem>().character = player;
            camObj.GetComponent<CameraSystem>().playerActive = true;
        }
    }


    IEnumerator Cutscene0()
    {
        // Moves player to location
        player.GetComponent<Transform>().position = new Vector3(-698, -95, 0);

        // Plays music
        music.Play();

        // Stops player movement
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<Player_Move>().enabled = false;

        yield return new WaitForSeconds(1);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Intro camera sweep
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().Play("c_dream_intro");
        yield return new WaitForSeconds(0.1f);

        // Fade in
        StartCoroutine(Fade(-1));

        // Wait for end of animation
        yield return new WaitForSeconds(camObj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
 
        // Sets camera to focus on player
        camObj.GetComponent<CameraSystem>().character = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        yield return new WaitForSeconds(0.5f);

        // INTRO ---------------------------------------------------------------------------------------------------
        // Move character right
        direction = new Vector2(4, 0);
        speed = 1;
        yield return StartCoroutine(MoveCharacter(player, 2));

        // Sets dialogue box to move onscreen
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);

        // Create opening dialogue
        player.GetComponent<Dialogue_Start>().StartConversation(0);

        // Waits until dialogue is finished
        yield return new WaitUntil(() => GameObject.Find("Player_Dialogue0").GetComponent<Dialogue_Manager>().completed);
        yield return new WaitForSeconds(0.1f);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");
        yield return new WaitForSeconds(1);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime+3);
    }


    IEnumerator MoveCharacter(GameObject player, float animLength)
    {
        player.GetComponent<Animator>().SetBool("IsRunning", true);
        active = true;
        yield return new WaitForSeconds(animLength);
        active = false;
        player.GetComponent<Animator>().SetBool("IsRunning", false);
    }

    // Moves the player
    void FixedUpdate()
    {
        if (active)
        {
            player.GetComponent<Transform>().Translate(direction * speed * Time.deltaTime);
        }
    }

    void Faceplayer(GameObject player, bool face)
    {
        if (face)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            player.GetComponent<BoxCollider2D>().offset = new Vector2(0.2f, player.GetComponent<BoxCollider2D>().offset.y);
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
            player.GetComponent<BoxCollider2D>().offset = new Vector2(-0.2f, player.GetComponent<BoxCollider2D>().offset.y);
        }
    }
}
