using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyMelee : MonoBehaviour
{
    public EnemyMovement enemyMovement;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            Debug.Log("IN RANGE");
            enemyMovement.inHitRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            Debug.Log("OUT RANGE");
            enemyMovement.inHitRange = false;
        }
    }
}
