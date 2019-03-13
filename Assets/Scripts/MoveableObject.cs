using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveableObject : MonoBehaviourPun, IPunObservable
{

    private Rigidbody2D rb2d;

    [HideInInspector]
    public NetworkStr Network;
    public struct NetworkStr
    {
        public Vector2 position;
        public float rotation;
    }

    void Awake(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(!photonView.IsMine){
            rb2d.position = Vector2.MoveTowards(rb2d.position, Network.position, Time.fixedDeltaTime);
            rb2d.rotation = Network.rotation;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(rb2d.position);
            stream.SendNext(rb2d.rotation);
            stream.SendNext(rb2d.velocity);
            stream.SendNext(rb2d.angularVelocity);
        }
        else{
            Network.position = (Vector2)stream.ReceiveNext();
            Network.rotation = (float)stream.ReceiveNext();
            rb2d.velocity = (Vector2)stream.ReceiveNext();
            rb2d.angularVelocity = (float)stream.ReceiveNext();
        }
    }
}
