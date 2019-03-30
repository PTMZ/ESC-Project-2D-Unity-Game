using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEscape : MonoBehaviour
{
    public EnemyMovement enemyMovement;

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            enemyMovement.changePatrol(true);
        }
    }
}
