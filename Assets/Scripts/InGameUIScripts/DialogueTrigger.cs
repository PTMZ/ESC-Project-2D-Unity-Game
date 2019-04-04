using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{ 
    public Dialogue dialogue;

    void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerDialogue()
    {
        StartCoroutine(wait());
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator wait()
    {
        yield return null;
    }
}
