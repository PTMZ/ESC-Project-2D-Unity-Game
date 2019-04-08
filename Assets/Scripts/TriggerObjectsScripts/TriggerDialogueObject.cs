using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueObject : MonoBehaviour
{
    public DialogueTrigger dialogue;
    public GameObject player;
    public int num;

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.GetComponent<BulletScript>() != null && OfflineGameManager.instance.storyProgress == num){
            dialogue.TriggerDialogue();
        }
    }
}
