using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;

    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;


    private void Awake(){
        if(PhotonRoom.room == null){
            PhotonRoom.room = this;
        } else {
            if(PhotonRoom.room != this){
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable(){
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 6;
        timeToStart = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(MultiplayerSettings.multiplayerSettings.delayStart){
            if(playersInRoom == 1){
                RestartTimer();
            }
            if(!isGameLoaded){
                if(readyToStart){
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if(readyToCount){
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                if(timeToStart <= 0){
                    StartGame();
                }
            }
        }
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        Debug.Log("Join success");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        if(MultiplayerSettings.multiplayerSettings.delayStart){
            if(playersInRoom > 1){
                readyToCount = true;
            }
            if(playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers){
                readyToStart = true;
                if(!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else {
            Debug.Log("Starting game...");
            StartGame();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom ++;
        if(MultiplayerSettings.multiplayerSettings.delayStart){
            if(playersInRoom > 1){
                readyToCount = true;
            }
            if(playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers){
                readyToStart = true;
                if(!PhotonNetwork.IsMasterClient){
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    void StartGame(){
        isGameLoaded = true;
        if(!PhotonNetwork.IsMasterClient)
            return;
        
        if(MultiplayerSettings.multiplayerSettings.delayStart){
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        Debug.Log("LoadLevel...");
        PhotonNetwork.LoadLevel(MultiplayerSettings.multiplayerSettings.multiplayerScene);
    }

    void RestartTimer(){
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;

    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneFinishedLoading");
        currentScene = scene.buildIndex;
        if(currentScene == MultiplayerSettings.multiplayerSettings.multiplayerScene){
            isGameLoaded = true;

            if(MultiplayerSettings.multiplayerSettings.delayStart){
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else{
                RPC_CreatePlayer();
            }
        }
    }


    [PunRPC]
    private void RPC_LoadedGameScene(){
        Debug.Log("RPC loaded game scene");
        playerInGame ++;
        if(playerInGame == PhotonNetwork.PlayerList.Length){
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer(){
        Debug.Log("Creating Player");
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), transform.position, Quaternion.identity, 0);
    }
}
