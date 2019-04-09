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

    public int storyProgActivate;
    public int curStoryProg;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
        target.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curStoryProg = OfflineGameManager.instance.storyProgress;
        if(OfflineGameManager.instance.storyProgress == storyProgActivate){
            isActivated = true;
            GetComponent<MachineAttack>().isActivated = true;
        }
        if(!isActivated){
            return;
        }
        target.transform.position = player.transform.position;
    }
    
    void FixedUpdate(){
        if(myself.getDead()){
            return;
        }
        Vector3 updPos = mypos.position - transform.position;
        myself.change = updPos;

        transform.position = mypos.position;
    }


}
