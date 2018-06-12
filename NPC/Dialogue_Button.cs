using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* DIALOGUE TUTORIAL:
 * 
 * Choices Note: The # at the end of the "Name_Choice0_#" = the dialogue to jump to
 * NOTE: If a choice is next, END must also be set to TRUE.
 * 
 * Duplicating:
 * 1: In (Characters > NPCs), Ctrl+D an already existing NPC
 * 2: Rename to new, UNIQUE name (unique to the scene)
 * 3: In Dialogue, duplicate "Name_D" from existing
 * 4: Rename all the names to character's name (if character has a space, include it)
 * IF CHOICES
 *      5: Go to what dialogue block has a choice after it and press "Is Choice Next"
 *      6: Set "Choice Index" to what choice it is (Name_Choice(#) in Canvas)
 *      7: Duplicate "Name_Choices" in (Canvas > Choices > Name_Choices)
 *      8: Rename all the names to character's name
 *      9: On each button, go to "On Click ()" and set: 
 *            1: # to what choice it is (Name_Choice(#))
 *            2: # of what dialogue you want to jump to
 *            3: Name of character
 *      10: Go inside button and click "text". Edit the choice sentence.
 * 
 * 
 * Prefab:
 * 1: In (Characters > NPCs), drag "NPC" from prefab
 * 2: Rename to new, UNIQUE name (unique to the scene)
 * 3: Look down to "Dialogue_Start", drag in the dialogueBox, dialogueName, and dialogueText.
 * 4: In Dialogue, drag in "_D" from prefab
 * 5: Place character's name before every "_" (if character has a space, include it)
 * 6: In "Dialogue_Manager" on "_Dialogue0", drag in dialogueName & dialogueText
 * IF CHOICES
 *      7: Go to what dialogue block has a choice after it and press "Is Choice Next"
 *      8: Set "Choice Index" to what choice it is (Name_Choice(#) in Canvas)
 *      9: In (Canvas > Choices), drag "_Choices" from prefab.
 *      10: Place character's name before first "_"
 *      11: On each button, go to "On Click ()", create 3, and drag in "Choice Script" in "Choices"
 *      12: 1st function is (Dialogue_Button > SetChoiceNum). 
 *          2nd is (Dialogue_Button > SetJumpIndex).
 *          3rd is (Dialogue_Button > TaskOnClick).
 *      13: Set:
 *            1: # to what choice it is (Name_Choice(#))
 *            2: # of what dialogue you want to jump to
 *            3: Name of character
 *      14: Go inside button and click "text". Edit the choice sentence.
 *      
 *      
 * FLAGS:
 * 1: To end a dialogue box without sequentially moving on, check the "end" bool on each dialogue box.
 * 2: To jump to a specific dialogue box when initiating a talk, select the NPC, and set the index.
 * 3: When choosing, on the dialogue block, add a component "Dialogue_Flag". Set index and name of 
 *    person you want their dialogue to skip to when you first talk to them. You can also use this
 *    to set the index on themselves so that when you talk to them again, they have different 
 *    dialogue.
 * 4: If want to check if you talked to everyone or just a specific person, add "Dialogue_Complete"
 *    from the prefab. Set the # of people and insert the dialogue you want to check if it is complete.
 */

public class Dialogue_Button : MonoBehaviour
{
    private int choiceNum = 0;
    private int jumpToIndex = 0;

    // choiceNum = disables the specific set of choices
    public void SetChoiceNum(int num)
    {
        choiceNum = num;
    }


    // jumpToIndex = Used to display a certain conversation line in Dialogue start
    public void SetJumpIndex(int index)
    {
        jumpToIndex = index;
    }

    // Must insert name of NPC in the choice button (diaName)
    public void TaskOnClick(string diaName)
    {
        // Disables the buttons
        GameObject.Find(diaName + "_Choices").GetComponent<Choice_Active>().Activate(diaName + "_Choice" + choiceNum, false);

        // Starts the dialogue box at the index
        GameObject.Find(diaName).GetComponent<Dialogue_Start>().StartConversation(jumpToIndex);
    }
}
