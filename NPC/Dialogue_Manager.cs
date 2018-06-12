using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {

    public bool completed;
    public Color textColor = Color.white;
    public GameObject character;
    public Text nameText;
    public Text dialogueText;
    public float dialogueSpeed = 0.03f;
    public bool isChoiceNext = false;
    public int choiceIndex = 0;
    public int jumpTo = -1; // Index to jump to after current dialogue is complete. -1 means to just order sequentially down.
    public bool end = false;

    private bool typeComplete;

    public Dialogue dialogue;
    private Queue<string> sentences;


    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue()
    {
        completed = false;
        typeComplete = false;
        nameText.text = dialogue.name;

        // Clears all previous sentences
        sentences.Clear();

        // Sets color of the text
        dialogueText.color = textColor;

        // Loops through all the sentence strings in the sentences queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        // Displays first sentence
        DisplayNextSentence();

        // Waits for keyboard input
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        while (sentences.Count != 0)
        {
            yield return StartCoroutine(WaitForKeyDown(KeyCode.E));
            DisplayNextSentence();
        }

        // Once all sentences are done, wait for player to press E to exit
        yield return StartCoroutine(WaitForKeyDown(KeyCode.E));

        completed = true;

        // If flagged, then change the dialogue index for another person
        if (gameObject.GetComponent("Dialogue_Flag") != null)
        {
            gameObject.GetComponent<Dialogue_Flag>().SetIndex();
        }
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        // Waits for the end of the sentence to finish typing
        while (!typeComplete)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Awaits for a key input
        while (!Input.GetKeyDown(keyCode) && !Input.GetKeyDown(KeyCode.Space))
            yield return null;

        typeComplete = false;
    }

    public void DisplayNextSentence()
    {
        // Displays the sentence
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        // Starts talking animation
        character.GetComponent<Animator>().Play("Player_Talking");

        dialogueText.text = "";

        // Loops through each character & displays it
        foreach (char letter in sentence.ToCharArray())
        {
            // Appends each letter to the string
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
            yield return null;
        }

        typeComplete = true;

        // Starts idle animation
        character.GetComponent<Animator>().Play("Player_Idle");
    }
}
