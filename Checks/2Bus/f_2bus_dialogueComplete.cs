using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f_2bus_dialogueComplete : MonoBehaviour {

    public GameObject cutscene;
    public Objects diagComplete; // Holds dialogues that need to be completed

    private bool completed = false;

    // Update is called once per frame
    void Update()
    {
        if (!completed)
        {
            bool done = false;

            // This part checks if all dialogue is complete
            for (int i = 0; i < diagComplete.objects.Length; i++)
            {
                if (!diagComplete.objects[i].GetComponent<Dialogue_Manager>().completed)
                {
                    done = false;
                    break;
                }
                else
                {
                    done = true;
                }
            }

            if (done)
            {
                completed = true;
                cutscene.GetComponent<c_2bus_1>().StartCutscene();
            }
        }
    }
}
