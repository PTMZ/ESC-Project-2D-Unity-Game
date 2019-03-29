using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=XnKKaL5iwDM 

public class Pickups : MonoBehaviour
{

    public int type;
    private OfflineGameManager offlineGM;

    public void Start(){
        offlineGM = FindObjectOfType<OfflineGameManager>();
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
                Destroy(gameObject);
            }
            if(type == 1){
                offlineGM.UpdateWeapon(1);
            }
            if(type == 2){
                offlineGM.UpdateWeapon(0);
            }
            //SceneManager.LoadScene("ConstrictLevel");
        }

        if (other.CompareTag("Constrict"))
        {
            Destroy(gameObject);
        }
    }
}