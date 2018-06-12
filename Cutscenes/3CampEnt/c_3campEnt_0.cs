using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_3campEnt_0 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public GameObject rain;
    public GameObject bus;
    public GameObject chars;
    public GameObject leslie;
    public GameObject ayla;
    public GameObject teleport;
    public bool skip = false;

    private bool isRunning = false;


    // Starts opening cutscene
    void Start()
    {
        if (!skip)
            StartCoroutine(Cutscene0());
        else
        {
            StartCoroutine(Fade(-1));
            player.GetComponent<Transform>().position = new Vector3(-465.75f, -81.63f, 0);
            camObj.GetComponent<CameraSystem>().character = player;
            camObj.GetComponent<CameraSystem>().playerActive = true;
            chars.SetActive(true);
            leslie.GetComponent<Transform>().position = new Vector3(-23.123f, -1.2332f, 0);
            teleport.SetActive(true);
        }
    }


    IEnumerator Cutscene0()
    {
        // Moves player to location
        player.GetComponent<Transform>().position = new Vector3(-693.58f, -95.68394f, 0);

        // Bus anim
        bus.GetComponent<Animator>().Play("b_3campEnt_0");

        yield return new WaitForSeconds(0.1f);

        // Enables rain from camera
        rain.SetActive(true);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        // Intro camera sweep
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().Play("c_3campEnt_intro");
        yield return new WaitForSeconds(0.1f);

        // Fade in
        StartCoroutine(Fade(-1));

        // Wait for end of animation
        yield return new WaitForSeconds(bus.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        // Fade out
        StartCoroutine(Fade(1));

        yield return new WaitForSeconds(3);

        // Moves player to location
        player.GetComponent<Transform>().position = new Vector3(-465.75f, -81.63f, 0);
        player.GetComponent<SpriteRenderer>().flipX = true;

        // Enables characters
        chars.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // Sets camera to focus on player
        camObj.GetComponent<CameraSystem>().character = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        gameObject.GetComponent<Cutscene_Fade>().enabled = false;
        gameObject.GetComponent<Cutscene_Fade>().enabled = true;
        yield return new WaitForSeconds(0.1f);

        // Fade in
        StartCoroutine(Fade(-1));

        yield return new WaitForSeconds(1);

        // Sets camera to look at object
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().enabled = false;
        StartCoroutine(lerpPosition(camObj.transform.position, new Vector3(leslie.transform.position.x, leslie.transform.position.y, camObj.transform.position.z), 2.0f));

        yield return new WaitForSeconds(1);

        // DIALOGUE --------------------------------------------------------------------------
        // Sets dialogue box to move onscreen
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);

        // Create opening dialogue
        leslie.GetComponent<Dialogue_Start>().StartConversation(0);

        // Waits until dialogue is finished
        yield return new WaitUntil(() => GameObject.Find("Leslie_Dialogue0").GetComponent<Dialogue_Manager>().completed);
        yield return new WaitForSeconds(0.1f);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;

        // Leslie animation walking to left
        leslie.GetComponent<SpriteRenderer>().flipX = true;
        leslie.GetComponent<Animator>().SetBool("IsRunning", true);
        isRunning = true;
  
        yield return new WaitForSeconds(2);

        // Sets camera to look at object
        StartCoroutine(lerpPosition(camObj.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, camObj.transform.position.z), 2.0f));

        yield return new WaitForSeconds(1);

        // Sets leslie's position
        isRunning = false;
        leslie.GetComponent<Transform>().position = new Vector3(-23.123f, -1.2332f, 0);
        leslie.GetComponent<Animator>().SetBool("IsRunning", false);

        // Letterbox disengage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");

        yield return new WaitForSeconds(1);

        // Sets camera to look at player
        camObj.GetComponent<Animator>().enabled = true;
        camObj.GetComponent<CameraSystem>().player = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        ayla.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitForSeconds(0.1f);

        // Enables player movement
        player.GetComponent<Player_Move>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        teleport.SetActive(true);
    }


    IEnumerator Fade(int index)
    {
        float fadeTime = gameObject.GetComponent<Cutscene_Fade>().BeginFade(index);
        yield return new WaitForSeconds(fadeTime + 3);
    }


    IEnumerator lerpPosition(Vector3 StartPos, Vector3 EndPos, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;
            camObj.transform.position = Vector3.Lerp(StartPos, EndPos, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
    }


    void Update()
    {
        if (isRunning)
        {
            leslie.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, leslie.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
