using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue8Script : MonoBehaviour
{
    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;
    public DialogueTrigger dialogue;

    // Update is called once per frame
    void Update()
    {
        if(laser1.activeSelf == false && laser2.activeSelf == false && laser3.activeSelf == false)
        {
            dialogue.TriggerDialogue();
        }
    }
}
