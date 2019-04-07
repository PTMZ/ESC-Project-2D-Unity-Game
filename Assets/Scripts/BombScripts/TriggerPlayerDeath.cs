using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerDeath : MonoBehaviour
{
    public AreaBomb areabomb;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            areabomb.changeBombState(true);
        }
    }
}
