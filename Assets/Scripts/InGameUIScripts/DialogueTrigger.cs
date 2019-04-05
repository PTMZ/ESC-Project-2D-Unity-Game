using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int SceneNumber;
    public Dialogue dialogue;

    //public OfflineGameManager OfflineGM;

    [HideInInspector]
    public int storyProgress;

    void Start()
    {
        storyProgress = OfflineGameManager.instance.storyProgress;
    }

    public void TriggerDialogue()
    {
        Debug.Log("Current scene is " + SceneNumber);
        Debug.Log("Current story is " + storyProgress);
        //Checks if is story dialogue
        if((SceneNumber) == storyProgress)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            storyProgress++;
            Debug.Log("Story Progressed.");
        }
        //else //if not story dialogue
        //{
        //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //}
    }

    IEnumerator wait()
    {
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerAvatar>().getIsDead())
            {
                return;
            }
            TriggerDialogue();
        }

    }
}
