using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use for activating or deactivating choice buttons
// Note: You CANNOT use the "Find" function when a gameObject is not active
public class Choice_Active : MonoBehaviour {

    public void Activate(string name, bool active)
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == name) { 
                child.gameObject.SetActive(active);
                return;
            }
        }
    }

}
