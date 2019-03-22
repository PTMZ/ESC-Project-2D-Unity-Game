using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class OfflineGameManager : MonoBehaviour
{

    public PlayerAvatar PlayerPrefab;

    [HideInInspector]
    public PlayerAvatar LocalPlayer;

    void Awake(){
        
    }

    void Start(){
        //PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        //LocalPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity).GetComponent<PlayerAvatar>();
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        
    }

    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */

    
}
