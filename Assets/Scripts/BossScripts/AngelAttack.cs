using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttack : MonoBehaviour
{

    public bool isActivated = false;
    public GameObject player;
    private PlayerAvatar pAvatar;

    public GameObject arrowPrefab;
    public float cooldown;
    public float timeout;
    public float randRadius;
    public static float damage = 10;
    private float cooldownTimeStamp;

    public Animator angelAnim;
    private Vector3 offsetY;
    private float curBulletSpeed;

    void Start()
    {
        angelAnim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        cooldownTimeStamp = Time.time;
        Debug.Log(player.transform.position);
        offsetY = new Vector3(0, 0.5f, 0);
        curBulletSpeed = arrowPrefab.GetComponent<AttackStats>().bulletSpeed / 2;
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
            Vector3 toPlayerDir = (playerMidPos - midPos).normalized;
            Vector3 toPlayerDirA = Rotate(toPlayerDir, 20);
            Vector3 toPlayerDirB = Rotate(toPlayerDir, -20);
            Vector3 upV = new Vector3(1, 0, 0);

            OfflineGameManager.instance.SpawnArrow(midPos + toPlayerDir*2, Quaternion.identity, toPlayerDir);
            OfflineGameManager.instance.SpawnArrow(midPos + toPlayerDirA*2, Quaternion.identity, toPlayerDirA);
            OfflineGameManager.instance.SpawnArrow(midPos + toPlayerDirB*2, Quaternion.identity, toPlayerDirB);
        }
        
    }

    public static Vector3 Rotate(Vector3 v, float degrees) {
         float radians = degrees * Mathf.Deg2Rad;
         float sin = Mathf.Sin(radians);
         float cos = Mathf.Cos(radians);
         
         float tx = v.x;
         float ty = v.y;
 
         return new Vector3(cos * tx - sin * ty, sin * tx + cos * ty, 0);
     }
}
