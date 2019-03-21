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
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, -1.7f, 0);
        player = GameObject.FindWithTag("Player");
        myself = GetComponent<EnemyAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myself.getDead()){
            return;
        }
        transform.position = mypos.position;
        if(player != null){
            target.transform.position = player.transform.position;
        }
    }
}
