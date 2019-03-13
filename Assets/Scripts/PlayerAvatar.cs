using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAvatar : MonoBehaviourPun
{

    private void Awake(){
        if(!photonView.IsMine && GetComponent<PlayerMovement>() != null){
            Destroy(GetComponent<PlayerMovement>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Animate stuff here
    }

    void FixedUpdate()
    {
        

    }

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
}
