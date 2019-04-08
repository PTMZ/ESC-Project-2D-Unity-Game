﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePaint : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerAvatar>().updateTrail(3);
        }
    }
}
