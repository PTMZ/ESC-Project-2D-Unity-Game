using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttack : MonoBehaviour
{

    public bool isActivated = false;
    private GameObject player;
    public GameObject constrictorPrefab;

    public GameObject arrowPrefab;
    private float cooldownTimeStamp;
    public float cooldown;

    private float cooldownTimeStamp2;
    public float cooldown2;

    public Animator angelAnim;
    private Vector3 offsetY;
    private float curBulletSpeed;
    public ParticleSystem particle;
    private bool particlePlaying = false;

    private string[] directionNames = {"up", "down", "left", "right"};

    void Start()
    {
        angelAnim = this.transform.Find("Staff").GetComponent<Animator>();
        particle = this.transform.Find("ConstrictingEffect").GetComponent<ParticleSystem>();
        player = GameObject.FindWithTag("Player");
        cooldownTimeStamp = Time.time;
        cooldownTimeStamp2 = Time.time;
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
        // Angel Arrows
        if(Time.time > cooldownTimeStamp){

            

            //Debug.Log("START SHOOT");
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

        if(Time.time > cooldownTimeStamp2){

            particlePlaying = true;
            angelAnim.Play("staff_red", -1);
            if (particlePlaying) {
                particle.Play();
                particlePlaying = false;
            }



            cooldownTimeStamp2 = Time.time + cooldown2;
            string dirName = directionNames[Random.Range(0, directionNames.Length)];
            Vector3 xOff = new Vector3(Random.Range(-7.0f, 7.0f), 0, 0);
            Vector3 yOff = new Vector3(Random.Range(-5.0f, 5.0f), 0, 0);



            if(dirName == "up"){
                var go = Instantiate(constrictorPrefab, xOff + new Vector3(0, -5, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(1,1,1);
                go.GetComponent<Constrictor>().direction = dirName;
                go.GetComponent<Constrictor>().isBoss = true;
                Destroy(go, 10.0f);
            }
            if(dirName == "down"){
                var go = Instantiate(constrictorPrefab, xOff + new Vector3(0, 8, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(1,1,1);
                go.GetComponent<Constrictor>().direction = dirName;
                go.GetComponent<Constrictor>().isBoss = true;
                Destroy(go, 10.0f);
            }
            if(dirName == "left"){
                var go = Instantiate(constrictorPrefab, yOff + new Vector3(7, 0, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(1,1,1);
                go.GetComponent<Constrictor>().direction = dirName;
                go.GetComponent<Constrictor>().isBoss = true;
                Destroy(go, 10.0f);
            }
            if(dirName == "right"){
                var go = Instantiate(constrictorPrefab, xOff + new Vector3(-7, 0, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(1,1,1);
                go.GetComponent<Constrictor>().direction = dirName;
                go.GetComponent<Constrictor>().isBoss = true;
                Destroy(go, 10.0f);
            }

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
