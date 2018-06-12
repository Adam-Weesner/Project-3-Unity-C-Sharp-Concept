using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choice_KeyPress : MonoBehaviour {

    public int choiceNum = 0;
    public int jumpToIndex = 0;
    public string diaName = "";

    public KeyCode key;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            // Changes color of text to different color
            ColorBlock cb = gameObject.GetComponent<Button>().colors;
            cb.normalColor = new Color(0f, 255f, 160f, 255f);
            gameObject.GetComponent<Button>().colors = cb;
        }
        else if (Input.GetKeyUp(key))
        {
            // Changes color of text back to normal
            ColorBlock cb = gameObject.GetComponent<Button>().colors;
            cb.normalColor = Color.white;
            gameObject.GetComponent<Button>().colors = cb;

            // Disables the buttons
            GameObject.Find(diaName + "_Choices").GetComponent<Choice_Active>().Activate(diaName + "_Choice" + choiceNum, false);

            // Starts the dialogue box at the index
            GameObject.Find(diaName).GetComponent<Dialogue_Start>().StartConversation(jumpToIndex);
        }
	}
}
