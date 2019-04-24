using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    //add story progress by 1
    //Stop all bgm, play boss fight bgm.
    //detect boss defeated, if defeated, change back to normal bgm, and add story progress. But not all bosses will add story progress, so ya.
    //Only  triggers pre-boss fight dialogue if any. 
    public GeneralPortal generalPortal;
    public string ThisBossTheme;
    public GameObject Boss;
    public DialogueTrigger dialogueBeforeBossFight; //after boss fight use a seperate dialoguetrigger. 

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerAvatar>().getIsDead())
            {
                return;
            }

            if (dialogueBeforeBossFight != null)
            {
                Debug.Log("Triggering Dialogue " + dialogueBeforeBossFight.SceneNumber);
                dialogueBeforeBossFight.TriggerDialogue();
            }
            else
            {
                OfflineGameManager.instance.storyProgress++;
            }
            if(generalPortal != null)
            {
                AudioManager.instance.Stop(generalPortal.curSceneTheme);
            }
            else
            {
                AudioManager.instance.StopAll();
            }
            
            AudioManager.instance.PlayTheme(ThisBossTheme);

        }

    }

    private int progressStoryCount = 0;
    void Update()
    {
        if(Boss == null && progressStoryCount == 0)
        {
            OfflineGameManager.instance.storyProgress++; //story progress added to trigger the next dialogue.
            progressStoryCount++;
        }
            
    }


}
