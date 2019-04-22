using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue10Door : MonoBehaviour
{
    public static bool BossActivated = false;
    public GameObject EnemyTriggered;


    void Update()
    {
        if (BossActivated)
        {
            EnemyTriggered.GetComponent<MachineMove>().levelSpecificTrigger = true;
        }
        //if (OfflineGameManager.instance.curHealth <= 0)
        //{
        //    //EnemyTriggered.GetComponent<MachineMove>().isActivated = false;
        //    //EnemyTriggered.GetComponent<MachineAttack>().isActivated = false;
        //    //EnemyTriggered.GetComponent<MachineMove>().levelSpecificTrigger = false;
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // EnemyTriggered.GetComponent<MachineMove>().levelSpecificTrigger = true;
            BossActivated = true;
        }
    }


}
