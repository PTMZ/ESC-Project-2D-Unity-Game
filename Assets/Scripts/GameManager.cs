using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    public PlayerAvatar PlayerPrefab;

    [HideInInspector]
    public PlayerAvatar LocalPlayer;

    void Awake(){
        if(!PhotonNetwork.IsConnected){
            SceneManager.LoadScene("Menu");
            return;
        }
    }

    void Start(){
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
}
