using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue0Script : MonoBehaviour
{
    //public Animator animator;
    public int dialogueCount = 0;
    public PlayerAvatar player;

    public void incrementDialogueCount()
    {
        if (dialogueCount == 0 && OfflineGameManager.instance.storyProgress<2)
        {
            player.TriggerQuestionMark();
        }
        dialogueCount++;
    }

}
