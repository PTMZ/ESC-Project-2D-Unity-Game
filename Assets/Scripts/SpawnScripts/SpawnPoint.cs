using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    public int pointIndex;
    public GameObject playerPrefab;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //sets the next spawn point to current point.
            OfflineGameManager.instance.spawnPoints[SceneManager.GetActiveScene().name] = pointIndex;
        }
    }

    public void spawnPlayer()
    {
        //Debug.Log("Player spawn method called.");
        if (player == null)
        {
            Debug.Log("No player object");
            //Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        player.transform.position = transform.position;
    }



}
