using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToSneakyLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        int count = 0;

        if ((other.CompareTag("Cone")))
        {
            count += 1;
        }
        if (count == 4)
        {
            SceneManager.LoadScene("SneakyLevel");
        }
    }
}
        /*if ((other.CompareTag("Cone")))

        {
            if ((other.CompareTag("Cone1")))

            {
                if ((other.CompareTag("Cone2")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }

        if ((other.CompareTag("Cone")))

        {
            if ((other.CompareTag("Cone2")))

            {
                if ((other.CompareTag("Cone1")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }

        if ((other.CompareTag("Cone1")))

        {
            if ((other.CompareTag("Cone0")))

            {
                if ((other.CompareTag("Cone2")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }

        if ((other.CompareTag("Cone1")))

        {
            if ((other.CompareTag("Cone2")))

            {
                if ((other.CompareTag("Cone0")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }

        if ((other.CompareTag("Cone2")))

        {
            if ((other.CompareTag("Cone0")))

            {
                if ((other.CompareTag("Cone1")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }

        if ((other.CompareTag("Cone2")))

        {
            if ((other.CompareTag("Cone1")))

            {
                if ((other.CompareTag("Cone0")))

                {
                    SceneManager.LoadScene("SneakyLevel");
                }
            }
        }*/


            /*if (other.CompareTag("Player"))

            {
                SceneManager.LoadScene("PuzzleLevel");
            }*/
    


