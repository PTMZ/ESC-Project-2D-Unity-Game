using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPaint : MonoBehaviour
{
    public int colourNum;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<PlayerAvatar>().updateTrail(colourNum);
        }

        if(other.CompareTag("DestroyableBox")){
            Debug.Log("Paint Box");
            other.gameObject.GetComponent<Destroyable>().changeColour(colourNum);
        }
    }
}

