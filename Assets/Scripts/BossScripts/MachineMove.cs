using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMove : MonoBehaviour
{
    public Transform mypos;
    public GameObject target;
    private GameObject player;
    private EnemyAvatar myself;

    public bool isActivated = false;

    //Used for situational specific triggers.
    public bool levelSpecificTrigger = false;


    public int storyProgActivate;
    public int curStoryProg;

    private Vector3 midPos;
    private Vector3 offsetY;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        target.transform.position = transform.position;

        offsetY = new Vector3(0, 1.5f, 0);
        midPos = transform.position + offsetY;

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        curStoryProg = OfflineGameManager.instance.storyProgress;
        if(OfflineGameManager.instance.storyProgress >= storyProgActivate && !isActivated){
            isActivated = true;
            GetComponent<MachineAttack>().isActivated = true;
            levelSpecificTrigger = false;
            //Debug.Log("attack activated.");
        }
        if(!isActivated){
            return;
        }
        if(player){
            target.transform.position = player.transform.position;
        }
    }
    
    void FixedUpdate(){
        if(myself.getDead()){
            //Debug.Log("Machine boss dead?.");
            if(isActivated){
                GetComponent<MachineAttack>().isActivated = false;
                isActivated = false;
            }
            return;
        }
        //Debug.Log("mypos: " + mypos.position);
        //Debug.Log("midPos: " + midPos);
        Vector3 updPos = mypos.position - midPos;
        myself.change = updPos;
        //Debug.Log(myself.change);

        //midPos = mypos.position;
        midPos += updPos / 2;
        transform.position = midPos - offsetY;
    }


}
