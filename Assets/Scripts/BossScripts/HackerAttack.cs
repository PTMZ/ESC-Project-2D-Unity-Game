using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerAttack : MonoBehaviour
{

    public bool isActivated = false;
    public GameObject player;
    private PlayerAvatar pAvatar;

    public GameObject turretPrefab;
    public float cooldown;
    public float timeout;
    public float randRadius;
    public static float damage = 10;
    private float cooldownTimeStamp;

    public Animator angelAnim;
    private Vector3 offsetY;

    private GameObject[] turretList;
    public int numTurrets;

    void Start()
    {
        angelAnim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        cooldownTimeStamp = Time.time;
        Debug.Log(player.transform.position);
        offsetY = new Vector3(0, 0.5f, 0);

        turretList = new GameObject[3];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isActivated){
            return;
        }
        if(Time.time > cooldownTimeStamp){
            Debug.Log("START SHOOT");
            //angelAnim.Play("machineboss_attack", -1);
            cooldownTimeStamp = Time.time + cooldown;
            //SpawnTargets();
            //DelayLaserAttack();

            //Shoot here
            Vector3 midPos = transform.position + offsetY;
            Vector3 playerMidPos = player.transform.position + offsetY;
            
            if(Time.time > cooldownTimeStamp){
                //Debug.Log("START SHOOT");
                //machineAnim.Play("machineboss_attack", -1);
                cooldownTimeStamp = Time.time + cooldown;
                //SpawnTargets();
                //DelayLaserAttack();
                SpawnTurrets();
            }
        }
        
    }

    private void SpawnTurrets(){
        foreach(GameObject t in turretList){
            if(t != null){
                Destroy(t);
            }
        }
        for(int i=0; i<numTurrets; i++){
            Vector3 randPos = new Vector3(0,0,0);
            turretList[i] = Instantiate(turretPrefab, randPos, Quaternion.identity);
        }
    }
}
