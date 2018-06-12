using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_2bus_1 : MonoBehaviour {

    public GameObject camObj;
    public GameObject player;
    public GameObject letterbox;
    public GameObject nic;
    public GameObject trigger;

    // Use this for initialization
    public void StartCutscene() {
        StartCoroutine(Cutscene0());
    }


    IEnumerator Cutscene0()
    {
        yield return new WaitForSeconds(0.1f);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // Letterbox engage
        letterbox.GetComponent<Animator>().Play("letterbox_engaged");

        yield return new WaitForSeconds(0.1f);

        // Sets trigger on
        trigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        // Sets camera to look at object
        camObj.GetComponent<CameraSystem>().playerActive = false;
        camObj.GetComponent<Animator>().enabled = false;
        StartCoroutine(lerpPosition(camObj.transform.position, new Vector3(nic.transform.position.x, nic.transform.position.y, camObj.transform.position.z), 2.0f));

        yield return new WaitForSeconds(0.1f);

        // DIALOGUE ---------------------------------------------------------------------------
        // Sets dialogue box to move onscreen
        player.GetComponent<Dialogue_Start>().animator.SetBool("isOpen", true);

        // Create opening dialogue
        nic.GetComponent<Dialogue_Start>().StartConversation(1);

        // Waits until dialogue is finished
        yield return new WaitUntil(() => GameObject.Find("Nic_Dialogue1").GetComponent<Dialogue_Manager>().completed);
        yield return new WaitForSeconds(0.1f);

        // Stops player movement
        player.GetComponent<Player_Move>().enabled = false;

        // Sets camera to look at object
        StartCoroutine(lerpPosition(camObj.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, camObj.transform.position.z), 2.0f));

        // Letterbox disengage
        letterbox.GetComponent<Animator>().Play("letterbox_disengaged");

        yield return new WaitForSeconds(2);

        // Sets camera to look at object
        camObj.GetComponent<Animator>().enabled = true;
        camObj.GetComponent<CameraSystem>().player = player;
        camObj.GetComponent<CameraSystem>().playerActive = true;

        yield return new WaitForSeconds(0.1f);

        // Enables player movement
        player.GetComponent<Player_Move>().enabled = true;
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
}
