using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=XnKKaL5iwDM 

public class Pickups : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAvatar>().getPoints();
            Destroy(gameObject);
            //SceneManager.LoadScene("ConstrictLevel");
        }
    }
}