using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{//only used for scenes with more than one spawn points

    //index 0 is first portal, 1 is second portal, counting from the left. 
    public SpawnPoint[] spawnPoints;
    private int index;


    void Start()
    {
        if (OfflineGameManager.instance.spawnPoints[SceneManager.GetActiveScene().name] == -1)
            return;

        index = OfflineGameManager.instance.spawnPoints[SceneManager.GetActiveScene().name];
        spawnPoints[index].spawnPlayer();
    }

    //public void respawnPlayer()
    //{
    //    index = OfflineGameManager.instance.spawnPoints[SceneManager.GetActiveScene().name];
    //    spawnPoints[index].spawnPlayer();
    //}
}
