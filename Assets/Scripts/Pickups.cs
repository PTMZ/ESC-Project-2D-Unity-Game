using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=XnKKaL5iwDM 

public class Pickups : MonoBehaviour
{

    public int type;
    private OfflineGameManager offlineGM;
    public GameObject[] toActivate;
    public GameObject[] toDeactivate;
    public DialogueTrigger dialogue;
    //public GameObject laser;

    public void Start(){
        offlineGM = FindObjectOfType<OfflineGameManager>();
        //laser.SetActive(true);
    }

    private void Update()
    {
        //transform.Rotate(0, 0, 90 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(type == 0){
                other.GetComponent<PlayerAvatar>().getPoints();
                AudioManager.instance.Play("HPPickup");
                Destroy(gameObject);
            }
            if(type == 1){
                AudioManager.instance.Play("FormPickup");
                other.GetComponent<PlayerAvatar>().changeAnimNinja();
                if (offlineGM.instance.storyProgress < 50)
                    OfflineGameManager.maxHealth += 50;
                OfflineGameManager.instance.curHealth = OfflineGameManager.maxHealth;
                Destroy(gameObject);
            }
            if(type == 2){
                AudioManager.instance.Play("FormPickup");
                offlineGM.UpdateWeapon(0);
                Destroy(gameObject);
            }
            if(type == 3){
                GameObject door = GameObject.Find("MyDoor");
                door.SetActive(false);
                Destroy(gameObject);
            }
            if(type == 4){
                foreach(GameObject g in toActivate){
                    g.SetActive(true);
                }
                foreach(GameObject g in toDeactivate){
                    g.SetActive(false);
                }
                //AudioManager.instance.Play("HPPickup");
                Destroy(gameObject);
            }

            /*
            if (type == 5)//key pickup
            {
                //other.GetComponent<PlayerAvatar>().getPoints();
                AudioManager.instance.Play("HPPickup");
                Destroy(gameObject);
            }
            */

            if (dialogue != null){
                dialogue.TriggerDialogue();
            }
            
            if(AstarPath.active){
                AstarPath.active.Scan();
            }

            /*
            if (SceneManager.GetActiveScene().name == "L2_AVHQ")
            {
                laser.SetActive(false);
            }
            */
            
            //SceneManager.LoadScene("ConstrictLevel");
        }

        if (other.CompareTag("Constrict"))
        {
            Destroy(gameObject);
        }
    }
}