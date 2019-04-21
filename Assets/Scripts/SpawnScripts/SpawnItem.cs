using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnItem : MonoBehaviour
{
    public GameObject destructiblePrefab;
    public GameObject destructible;

    //private IEnumerator coroutine;

    void spawnDestructible()
    {
        //Debug.Log("Destructible spawn method called.");
        //Debug.Log("No player object");
        destructible = Instantiate(destructiblePrefab, Vector3.zero, Quaternion.identity);
        destructible.transform.position = transform.position;
        //coroutine = DelaySeconds(1.5f);
        //StartCoroutine(coroutine);
    }

    //private int spawnCount;
    //IEnumerator DelaySeconds(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    if(destructible == null)
    //    {

    //        spawnCount++;
    //    }
        


    //}

    void Update()
    {
        if (destructible == null)
            spawnDestructible();

        //if(spawnCount >=1)
        //{
        //    StopCoroutine(coroutine);
        //}
    }

}
