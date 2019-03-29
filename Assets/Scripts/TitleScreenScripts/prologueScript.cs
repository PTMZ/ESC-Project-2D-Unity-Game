using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prologueScript : MonoBehaviour
{
    public Dialogue dialogue;
    public int c = 0;

    void Start()
    {
       //FindObjectOfType<prologueDialogueManager>().StartDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (c == 0)
        {
            FindObjectOfType<prologueDialogueManager>().StartDialogue(dialogue);
            c++;
        }

    }
}
