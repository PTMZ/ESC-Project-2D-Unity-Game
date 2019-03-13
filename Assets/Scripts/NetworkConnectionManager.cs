using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Button btnConnectMaster;
    public Button btnConnectRoom;

    public bool tryConnectMaster;
    public bool tryConnectRoom;

    // Start is called before the first frame update
    void Start()
    {
        tryConnectMaster = false;
        tryConnectRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        btnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !tryConnectMaster);
        btnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !tryConnectMaster && !tryConnectRoom);
    }

    public void OnClickConnectMaster(){
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "PlayerName";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "v1";

        tryConnectMaster = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickConnectRoom(){
        if(!PhotonNetwork.IsConnected){
            return;
        }

        tryConnectRoom = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause){
        base.OnDisconnected(cause);
        tryConnectMaster = false;
        tryConnectRoom = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        tryConnectMaster = false;
        Debug.Log("Connected to Master!");
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        tryConnectRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Playeres in room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        SceneManager.LoadScene("Main");
        //SceneManager.LoadScene("TestLevel");
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 20});
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        tryConnectRoom = false;
    }

}
