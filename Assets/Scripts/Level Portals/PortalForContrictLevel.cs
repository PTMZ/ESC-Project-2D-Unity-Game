using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalForContrictLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("PuzzleLevel");
        }
        if (other.CompareTag("Constrict"))
        {
            SceneManager.LoadScene("ConstrictLevel");
        }
    }
}
