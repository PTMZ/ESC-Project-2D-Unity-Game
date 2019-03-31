using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToSneakyLevel : MonoBehaviour
{

    bool withCone = false;
    static int count = 0;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Cone"))){
            Debug.Log("CONE IN");
            withCone = true;
            count ++;
        }
        if(count == 3){
            SceneManager.LoadScene("B2_AVHQ");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Cone"))){
            withCone = true;
            count --;
        }
    }
    
}


