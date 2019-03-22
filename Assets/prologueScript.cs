using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prologueScript : MonoBehaviour
{
    public Dialogue dialogue;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            count++;
        }
    }
}
