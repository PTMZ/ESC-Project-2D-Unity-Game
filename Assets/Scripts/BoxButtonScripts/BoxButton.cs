using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButton : MonoBehaviour
{
    //public ButtonTrail buttonTrail;
    private int count = 0;

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Destroyable")){
            count ++;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Destroyable")){
            count --;
        }
    }

    public bool getPressed(){
        return count>0;
    }
}
