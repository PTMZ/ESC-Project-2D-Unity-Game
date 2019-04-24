using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaserTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerAvatar>().getHit(MachineAttack.damage2);
            
        }
        if (other.CompareTag("Destroyable"))
        {
            other.gameObject.GetComponent<Destroyable>().getHit(MachineAttack.damage2);
        }
        Destroy(gameObject);
    }
}
