using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
    //Condition for before and after storyprogress
    //gameObjects to deactivate
    //gameObject to activate
    public int storyProgressToActivate;
    public GameObject[] toActivate;
    
    public int storyProgressToDeactivate;
    public GameObject[] toDeactivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (OfflineGameManager.instance.storyProgress <= storyProgressToActivate)
            {
                foreach (GameObject obj in toActivate)
                {
                    if (obj == null)
                        continue;
                    else
                    {
                        obj.SetActive(true);
                    }
                    
                }
            }
            //If story has already progress beyond the  
            if (OfflineGameManager.instance.storyProgress >= storyProgressToDeactivate)
            {
                foreach (GameObject obj in toDeactivate)
                {
                    if (obj == null)
                        continue;
                    else
                    {
                        Destroy(obj);
                    }

                }
            }

        }
    }

}
