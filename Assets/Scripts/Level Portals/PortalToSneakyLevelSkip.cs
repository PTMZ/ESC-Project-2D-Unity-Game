using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToSneakyLevelSkip : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("EnterPortal");
            SceneManager.LoadScene("B2_AVHQ");
        }
    }
}
