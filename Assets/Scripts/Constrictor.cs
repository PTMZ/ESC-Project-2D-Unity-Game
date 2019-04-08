using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrictor : MonoBehaviour
{
    public float speed;//speed of the game object

    Vector3 rightDir = new Vector3(100, 0, 0);

    private bool isHit = false;
    private float cooldownTimeStamp;
    public float cooldown = 0.1f;

    public float dmg = 100;
    private PlayerAvatar pAvatar;

    // Start is called before the first frame update
    void Start(){
        pAvatar = GameObject.FindWithTag("Player").GetComponent<PlayerAvatar>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + rightDir, Time.deltaTime * speed);
        if(isHit && Time.time > cooldownTimeStamp){
            cooldownTimeStamp = Time.time + cooldown;
            pAvatar.getHit(dmg);
        }
        
        void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
            isHit = true;
        }
        void OnTriggerExit2D(Collider2D other){
            if(other.CompareTag("Player"))
                isHit = false;
        }
    }
}


