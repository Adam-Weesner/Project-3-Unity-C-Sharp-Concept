using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Folders : MonoBehaviour {

    public GameObject dream;
    public GameObject bus;
    public GameObject campEntrance;

    void Start()
    {
        dream.SetActive(false);
        bus.SetActive(false);
        campEntrance.SetActive(false);
    }
}
