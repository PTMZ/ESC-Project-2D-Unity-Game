using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnItem : MonoBehaviour
{
    public GameObject destructiblePrefab;
    public GameObject destructible;



    void spawnDestructible()
    {
        //Debug.Log("Destructible spawn method called.");
            //Debug.Log("No player object");
            destructible = Instantiate(destructiblePrefab, Vector3.zero, Quaternion.identity);
            destructible.transform.position = transform.position;  
    }

    void Update()
    {
        if (destructible == null)
            spawnDestructible();
    }

}
