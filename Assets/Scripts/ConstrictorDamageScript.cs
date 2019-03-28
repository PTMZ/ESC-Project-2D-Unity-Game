using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrictorDamageScript : MonoBehaviour
{
    float ConstrictorDmg = 50;
    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        if(col.gameObject.GetComponent<PlayerMovement>() != null){
            return;
        }
        */
        if (col.gameObject.GetComponent<PlayerAvatar>() != null)
        {
            Debug.Log("I am hit, my name is " + col.gameObject.name);
            col.gameObject.GetComponent<PlayerAvatar>().getHit(ConstrictorDmg);
        }

        if (col.gameObject.GetComponent<EnemyAvatar>() != null)
        {
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<EnemyAvatar>().getHit(ConstrictorDmg);
        }
    }
}
