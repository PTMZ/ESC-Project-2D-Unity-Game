using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HackerTurret : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 nearestPlayerPos;
    private float cooldownTimeStamp;
    public float cooldown = 0.7f;
    void Start()
    {
        cooldownTimeStamp = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > cooldownTimeStamp){
            cooldownTimeStamp = Time.time + cooldown;
            FindNearestPlayer();
            Fire();
        }
    }

    void Fire(){
        Vector3 playerMid = nearestPlayerPos + new Vector3(0, 0.5f, 0);
        Vector3 midPos = transform.position + new Vector3(0, 0.5f, 0);
        Vector3 bulletDir = (playerMid - midPos).normalized;
        if(!PhotonNetwork.IsConnected){
            OfflineGameManager.instance.SpawnEnemyBullet(midPos + bulletDir * 0.6f, transform.rotation, bulletDir);
            
        }
        else{
            OnlineGameManager.SpawnBullet(midPos + bulletDir * 0.6f, transform.rotation, bulletDir);
            Debug.Log("turret shoot");
        }
        //weaponAnim.weaponAnimator.Play("attack", -1);
    }

    void FindNearestPlayer(){
        if(PhotonNetwork.IsConnected){
            // online find nearest player, or random?
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            nearestPlayerPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        }
        else{
            var nearestPlayer = GameObject.FindWithTag("Player");
            if(nearestPlayer){
                nearestPlayerPos = nearestPlayer.transform.position;
            }
        }
    }
}
