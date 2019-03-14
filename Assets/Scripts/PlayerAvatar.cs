using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAvatar : MonoBehaviourPun, IPunObservable
{

    public float health = 100;


    // for animations //
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;




    private void Awake(){
        if(!photonView.IsMine && GetComponent<PlayerMovement>() != null){
            Destroy(GetComponent<PlayerMovement>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // for animations //
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        change = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        // Animate stuff here
        /*
        change = Vector3.zero;
        if(photonView.IsMine){
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
        }
        */
        UpdateAnimation();
        
    }

    void UpdateAnimation(){
        if (change != Vector3.zero){
            mySpriteRenderer.flipX = (change.x < 0);
            animator.SetBool("moving", true);
        }
        else{
            animator.SetBool("moving", false);
        }
    }

    void SyncAnimation(){

    }




    void FixedUpdate()
    {
        

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(change);
        }
        else{
            change = (Vector3)stream.ReceiveNext();
            UpdateAnimation();
        }

    }

    public void getHit(){
        health -= 10;
        Debug.Log("I am hit, health is = " + health);
    }

    /*
    public static void RefreshInstance(ref PlayerAvatar player, PlayerAvatar prefab){
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        if(player != null){
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }
        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<PlayerAvatar>();
    }
    */
}
