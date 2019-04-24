using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Constrictor : MonoBehaviour
{
    public float speed;//speed of the game object
    public string direction; //type "right", "left", "up", or "down" only

    Vector3 moveRight = new Vector3(100, 0, 0);
    Vector3 moveLeft = new Vector3(-100, 0, 0);
    Vector3 moveUp = new Vector3(0, 100, 0);
    Vector3 moveDown = new Vector3(0, -100, 0);

    private bool isHit = false;
    private bool isOnlineHit = false;
    private float cooldownTimeStamp;
    public float cooldown = 0.3f;

    private float dmg = 2;
    private PlayerAvatar pAvatar;

    public bool isBoss = false;

    // Start is called before the first frame update
    void Start(){
        
        cooldownTimeStamp = Time.time;
        if(PhotonNetwork.IsConnected){
            return;
        }
        if(!isBoss && OfflineGameManager.instance.storyProgress < 50){
            AudioManager.instance.PlayLoopButMustStop("Constrictor");
        }
        if(PhotonNetwork.IsConnected){
            pAvatar = PlayerManager.LocalPlayerInstance.GetComponent<PlayerAvatar>();
        }
        else{
            pAvatar = GameObject.FindWithTag("Player").GetComponent<PlayerAvatar>();
        }
    }

    void FixedUpdate()
    {
        if (direction == "right")
        {
            ConstrictorAction(moveRight);
        }
        else if(direction == "left")
        {
            ConstrictorAction(moveLeft);
        }
        else if(direction == "up")
        {
            ConstrictorAction(moveUp);
        }
        else if(direction == "down")
        {
            ConstrictorAction(moveDown);
        }

    }

    void ConstrictorAction(Vector3 dir)
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, Time.deltaTime * speed);
        /*
        if (isHit && Time.time > cooldownTimeStamp)
        {
            cooldownTimeStamp = Time.time + cooldown;
            if(PhotonNetwork.IsConnected){
                
            }
            else{
                pAvatar.getHit(dmg);
                Debug.Log("testing2");
            }
        }
        */
        if(PhotonNetwork.IsConnected){
            if(isOnlineHit && Time.time > cooldownTimeStamp){
                cooldownTimeStamp = Time.time + cooldown;
                //Debug.Log("HIT ONLINE PLAYER CD");
                if(pAvatar){
                    pAvatar.reduceHealthRPC(2);
                    //Debug.Log("HIT ONLINE PLAYER");
                }
            }
        }
        else{
            if(isHit && Time.time > cooldownTimeStamp){
                cooldownTimeStamp = Time.time + cooldown;
                if(pAvatar){
                    pAvatar.getHit(dmg);
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            isHit = true;
            if(PhotonNetwork.IsConnected && PhotonView.Get(other.gameObject).IsMine){
                pAvatar = other.gameObject.GetComponent<PlayerAvatar>();
                isOnlineHit = true;
                //Debug.Log("ONLINE HIT");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            isHit = false;
            if(PhotonNetwork.IsConnected && PhotonView.Get(other.gameObject).IsMine){
                isOnlineHit = false;
                //Debug.Log("ONLINE OUT OF HIT");
            }
        }
    }

    void OnTriggerStay2D(Collider2D other){
        
        /*
        if(other.CompareTag("Player")){
            if(PhotonNetwork.IsConnected && PhotonView.Get(other.gameObject).IsMine){
                other.gameObject.GetComponent<PlayerAvatar>().reduceHealthRPC(1);

            }
        }
        */
    }
}


