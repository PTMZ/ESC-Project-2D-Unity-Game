using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class activates or deactivates traps based on the story progress
public class EventTrigger : MonoBehaviour
{
    //Will not activate if -1
    public int storyProgressToActivate = -1;
    public GameObject[] onCollideActivate;
    
    public int storyProgressToDeactivate = -1;
    public GameObject[] onCollideDeactivate;

    public int onStartProgressActivate = -1;
    public GameObject[] onStartToActivate;

    public int onStartProgressDeactivate = -1;
    public GameObject[] onStartToDeactivate;



    void Start()
    {
        if(onStartProgressActivate != -1)
            if (OfflineGameManager.instance.storyProgress <= storyProgressToActivate)
            {
                foreach (GameObject obj in onStartToActivate)
                {
                    if (obj == null)
                        continue;
                    else
                    {
                        obj.SetActive(true);
                    }

                }
            }

        if (onStartProgressDeactivate != -1)
            if (OfflineGameManager.instance.storyProgress >= storyProgressToDeactivate)
            {
                foreach (GameObject obj in onStartToDeactivate)
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (storyProgressToActivate != -1)
                if (OfflineGameManager.instance.storyProgress <= storyProgressToActivate)
                {
                    foreach (GameObject obj in onCollideActivate)
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
            if (storyProgressToDeactivate != -1)
                if (OfflineGameManager.instance.storyProgress >= storyProgressToDeactivate)
                {
                    foreach (GameObject obj in onCollideDeactivate)
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
