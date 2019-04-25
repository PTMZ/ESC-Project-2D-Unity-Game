using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void selectScene()
    {
        if(this.gameObject.name == "B5_AVHQ"){ OfflineGameManager.instance.storyProgress = 0; }
        if(this.gameObject.name == "B4_AVHQ"){ OfflineGameManager.instance.storyProgress = 5; }
        if(this.gameObject.name == "B3_AVHQ"){ OfflineGameManager.instance.storyProgress = 7; }
        if(this.gameObject.name == "B2_AVHQ"){ OfflineGameManager.instance.storyProgress = 9; }

        if(this.gameObject.name == "PW_01"){ OfflineGameManager.instance.storyProgress = 11; }
        if(this.gameObject.name == "PW_02"){ OfflineGameManager.instance.storyProgress = 12; }
        if(this.gameObject.name == "PW_03"){ OfflineGameManager.instance.storyProgress = 0; }
        if(this.gameObject.name == "PW_04"){ OfflineGameManager.instance.storyProgress = 14; }

        if(this.gameObject.name == "MSP_01"){ OfflineGameManager.instance.storyProgress = 15; }
        if(this.gameObject.name == "MSP_02"){ OfflineGameManager.instance.storyProgress = 15; }
        if(this.gameObject.name == "MSP_03"){ toNinja(); }
        if(this.gameObject.name == "MSP_04"){ toNinja(); }

        if(this.gameObject.name == "L1_AVHQ"){ toNinja(); }
        if(this.gameObject.name == "L2_AVHQ"){ toNinja(); }
        if(this.gameObject.name == "L3_AVHQ"){ toNinja(); }
        if(this.gameObject.name == "L4_AVHQ"){ toNinja(); }

        if(this.gameObject.name == "L5_AngBoss"){ toNinja(); }
        if(this.gameObject.name == "L5_HackerBoss"){ toNinja(); }

        SceneManager.LoadScene(this.gameObject.name);
    }

    private void toNinja(){
        OfflineGameManager.instance.storyProgress = 51; 
        OfflineGameManager.instance.curAttack = 1;
        OfflineGameManager.maxHealth += 50;
        OfflineGameManager.instance.curHealth = OfflineGameManager.maxHealth;
    }
}
