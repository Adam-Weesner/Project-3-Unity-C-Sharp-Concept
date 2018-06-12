using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dialogue_Flag : MonoBehaviour {

    public string diaName;
    public int index;

    // When flag is activted, it will set name to speak on dialogue index #
    public void SetIndex()
    {
        GameObject.Find(diaName).GetComponent<Dialogue_Start>().index = index;
    }

}
