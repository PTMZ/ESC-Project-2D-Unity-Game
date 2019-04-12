using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int SceneNumber;
    public Dialogue dialogue;
    public int triggerLimit = 1;
    private int triggerCount = 0;
    //public OfflineGameManager OfflineGM;

    [HideInInspector]
    public int storyProgress;

    void Start()
    {
        //storyProgress = OfflineGameManager.instance.storyProgress;
    }

    public void TriggerDialogue()
    {
        Debug.Log("Current scene is " + SceneNumber);
        Debug.Log("Current story is " + OfflineGameManager.instance.storyProgress);
        //Checks if is story dialogue
        if ((SceneNumber) == OfflineGameManager.instance.storyProgress)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            OfflineGameManager.instance.storyProgress++;
            SaveGameScript.SaveGame();
            Debug.Log("Story Progressed. Game Saved. Story is now " + OfflineGameManager.instance.storyProgress);
        }
        else if(SceneNumber == -1)
        {
            if (triggerCount < triggerLimit)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                triggerCount++;
            }
            
            
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
            Debug.Log("Triggering Dialogue " + SceneNumber);
            TriggerDialogue();
        }

    }
}
