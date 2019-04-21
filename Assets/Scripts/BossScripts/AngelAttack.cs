using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttack : MonoBehaviour
{

    public bool isActivated = false;
    public GameObject player;
    private PlayerAvatar pAvatar;

    public GameObject targetPrefab;
    public GameObject laserHitPointPrefab;
    public float cooldown;
    public float timeout;
    public float randRadius;
    public static float damage = 10;
    private float cooldownTimeStamp;

    public Animator angelAnim;
    private Vector3 offsetY;

    void Start()
    {
        angelAnim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        cooldownTimeStamp = Time.time;
        Debug.Log(player.transform.position);
        offsetY = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isActivated){
            return;
        }
        if(Time.time > cooldownTimeStamp){
            Debug.Log("START SHOOT");
            angelAnim.Play("machineboss_attack", -1);
            cooldownTimeStamp = Time.time + cooldown;
            //SpawnTargets();
            //DelayLaserAttack();
        }
        
    }
}
