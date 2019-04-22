using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform mypos;
    public GameObject target;
    private GameObject player;
    private Vector3 offset;
    private EnemyAvatar myself;
    private PlayerAvatar pAvatar;

    [HideInInspector]
    public WeaponAnim weaponAnim;

    private float cooldownTimeStamp;
    public float cooldown = 0.5f;
    public float meleeDmg = 10;

    public Transform[] patrolPoints;
    private bool patrolState = true;
    private int curPatrol = 0;
    private float closeDist = 0.5f;

    private LayerMask maskLayer;
    public float visionRadius;
    public bool isBlocked = true;
    public bool inVisionRange = false;
    public bool inHitRange = false;

    public bool cd;
    private Vector3 midPos;

    public int enemyType = 0;

    private Rigidbody2D rb2d;



    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, -1.7f, 0);
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        rb2d = GetComponent<Rigidbody2D>();
        pAvatar = player.GetComponent<PlayerAvatar>();
        Debug.Log(pAvatar.points);
        weaponAnim = GetComponent<WeaponAnim>();
        if(patrolPoints.Length == 0){
            patrolState = false;
        }

        midPos = mypos.position;

        maskLayer = LayerMask.GetMask("TransparentFX");
    }

    // Update is called once per frame
    private void Update()
    {
        if(patrolState){
            target.transform.position = patrolPoints[curPatrol].position;
            if(reachedPatrol()){
                curPatrol = (curPatrol + 1) % patrolPoints.Length;
                Debug.Log("REACHED");
                Debug.Log(curPatrol);
            }
        }
        else{
            if (player != null)
            {
                target.transform.position = player.transform.position;
            }   
        }
    }


    private int triggerCount = 0;
    void FixedUpdate()
    {
        if(myself.getDead()){
            return;
        }
        Vector3 updPos = mypos.position - midPos;
        myself.change = updPos;
        
        //midPos = mypos.position;
        midPos += updPos/2;
        transform.position = midPos - new Vector3(0, 0.5f, 0);


        if (inHitRange){
            if(Time.time > cooldownTimeStamp){
                cooldownTimeStamp = Time.time + cooldown;
                Debug.Log("ENEMY ATTACK");
                if(enemyType == 0){
                    pAvatar.getHit(meleeDmg);   // Comment this line out to stop enemy from attacking
                    // Play attack animation here
                    AudioManager.instance.Play("GuardMelee");
                    weaponAnim.weaponAnimator.Play("attack", -1);
                }
                else if(enemyType == 1){
                    Vector3 playerMid = player.transform.position + new Vector3(0, 0.5f, 0);
                    Vector3 bulletDir = (playerMid - midPos).normalized;
                    OfflineGameManager.instance.SpawnEnemyBullet(midPos + bulletDir * 0.6f, transform.rotation, bulletDir);
                    target.transform.position = transform.position;
                    weaponAnim.weaponAnimator.Play("attack", -1);
                }
            }
        }
        cd = (Time.time > cooldownTimeStamp);
    }
    
    public void changePatrol(bool newState){
        patrolState = newState;
        if(patrolState == false){
            // Instantiate Exlamation mark here
        }
    }

    public bool checkBlocked(){
        RaycastHit2D hitInfo = Physics2D.Raycast(midPos, player.transform.position - midPos, visionRadius, maskLayer);
        isBlocked = (hitInfo.collider != null);
        return isBlocked;
    }

    public void TriggerExclamationMarkMethod(){
        if (triggerCount == 0)
        {
            myself.TriggerExclamationMark();
            triggerCount++;
        }
    }

    private bool reachedPatrol(){
        //Debug.Log((transform.position - patrolPoints[curPatrol].position).magnitude);
        return ( (midPos - patrolPoints[curPatrol].position).magnitude < closeDist );
    }
}
