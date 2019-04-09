using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue2Script : MonoBehaviour
{
    private bool keySpawned = false;
    private int sceneNum = 3;
    public GameObject key;

    void FixedUpdate()
    {
        if (OfflineGameManager.instance.storyProgress == sceneNum && !keySpawned)
        {
            key.SetActive(true);
            keySpawned = true;
        }
    }
}
