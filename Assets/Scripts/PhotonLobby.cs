using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    RoomInfo[] rooms;

    public GameObject offlineButton;
    public GameObject battleButton;
    public GameObject cancelButton;

    private void Awake(){
        lobby = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to master");
        PhotonNetwork.AutomaticallySyncScene = true;
        offlineButton.SetActive(false);
        battleButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log("Join failed, creating room...");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("Create room failed");
        CreateRoom();
    }

    void CreateRoom(){
        int randRoomName = Random.Range(0,10000);
        //RoomOptions roomOps = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers};
        RoomOptions roomOps = new RoomOptions(){ IsVisible = true, IsOpen = true, MaxPlayers = 10};
        PhotonNetwork.CreateRoom("Room" + randRoomName, roomOps);
    }

    public void OnBattleButtonClicked(){
        FindObjectOfType<AudioManager>().Play("Select");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCanceButtonClicked(){
        FindObjectOfType<AudioManager>().Play("Back");
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
