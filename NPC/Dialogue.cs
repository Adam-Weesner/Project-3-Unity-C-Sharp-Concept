using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable so that we can edit it in Unity
[System.Serializable]
public class Dialogue {

    public string name;

    // Sets area of Unity GUI input
    [TextArea(3, 10)]
    public string[] sentences;
}
