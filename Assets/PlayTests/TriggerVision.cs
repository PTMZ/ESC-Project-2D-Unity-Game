using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVision : MonoBehaviour
{

    public EnemyMovement enemyMovement;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            //enemyMovement.changePatrol(false);
            enemyMovement.inVisionRange = true;
        }
    }
}
