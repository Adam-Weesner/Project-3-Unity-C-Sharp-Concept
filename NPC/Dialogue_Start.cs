using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Start : MonoBehaviour
{
    // Name of the next dialog
    public bool noCollision = false;
    public Animator animator;
    public Text nameText;
    public Text dialogueText;
    public int index = 0;  // Use this to set what dialogue box you want to start with 

    public bool nextChoice = false; // false if there is a choice next, true if there is no choice (which will disable dialogueBox and enable movement

    private int tempIndex = 0;
    private bool preventRapidFire = true;

    // When player enters a NPC's space, then allow the player to interact.
    void OnTriggerStay2D(Collider2D trig)
    {
        // When player presses "e" near NPC, activate dialogue
        if (!noCollision && Input.GetButtonDown("Fire1") && preventRapidFire)
        {
            preventRapidFire = false;

            // Temporarily disables the player movement
            GameObject.Find("Player").GetComponent<Player_Move>().enabled = false;
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject.Find("Player").GetComponent<Animator>().SetBool("IsRunning", false);

            // Sets dialogue box to move onscreen
            animator.SetBool("isOpen", true);

            StartConversation(index);
        }
    }

    public void StartConversation(int index)
    {
        StartCoroutine(Selector(index));
    }

    IEnumerator Selector(int index)
    {
        for (int i = index; GameObject.Find(gameObject.name + "_Dialogue" + i) && !nextChoice; i++)
        {
            GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().StartDialogue();

            while (!GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().completed)
            {
                yield return new WaitForSeconds(0.1f);
            }

            // If the next dialogue is a choice
            if (GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().isChoiceNext)
            {
                nextChoice = true;
                tempIndex = i;
            }

            // If the next dialogue is a jump
            if (GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().jumpTo != -1)
            {
                i = GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().jumpTo-1;
            }

            // If the this dialog ends, MUST DEACTIVE END IF YOU ARE JUMPING
            else if (GameObject.Find(gameObject.name + "_Dialogue" + i).GetComponent<Dialogue_Manager>().end)
            {
                break;
            }
        }

        // If there is no choice next, enable dialogue box and movement
        if (nextChoice == false) { 
            // Sets dialogue box to move offscreen.
            animator.SetBool("isOpen", false);

            // Enables the player movement again
            GameObject.Find("Player").GetComponent<Player_Move>().enabled = true;
        }
        // If there is a choice, then show choices
        else
        {
            nameText.text = "";
            dialogueText.text = "";

            // Enables the buttons
            GameObject.Find(gameObject.name + "_Choices").GetComponent<Choice_Active>().Activate(gameObject.name + "_Choice" + GameObject.Find(gameObject.name + "_Dialogue" + tempIndex).GetComponent<Dialogue_Manager>().choiceIndex, true);
        }

        nextChoice = false;
        preventRapidFire = true;

        yield return null;
    }
}
