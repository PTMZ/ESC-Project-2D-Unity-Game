using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitPointTrigger : MonoBehaviour
{
    void Start(){
        Destroy(gameObject, 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerAvatar>().getHit(MachineAttack.damage);
        }
        if (other.CompareTag("Destroyable"))
        {
            other.gameObject.GetComponent<Destroyable>().getHit(MachineAttack.damage);
        }
        Destroy(gameObject);
    }
}
