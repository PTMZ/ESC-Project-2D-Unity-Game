using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrassDeath : MonoBehaviour
{
    public AreaBomb areabomb;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            areabomb.changeBombState(false);
        }
    }
}
