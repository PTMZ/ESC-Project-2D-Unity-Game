using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    static public OnlineGameManager Instance;
    public float curCooldown;

    public GameObject winText;

    [SerializeField]
    private PlayerAvatar playerPrefab;

    [SerializeField]
    private GameObject[] bulletPrefabs;

    [SerializeField]
    private int curAttack;

    [SerializeField]
    private Transform[] respawnPoints;

    void Awake(){
        if(!PhotonNetwork.IsConnected){
            SceneManager.LoadScene("TitleScreen");
            return;
        }
    }

    void Start(){
        Instance = this;

        if (!PhotonNetwork.IsConnected) {
            SceneManager.LoadScene("PvPLobby");
            return;
        }

        if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.
            Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else {
            if (PlayerManager.LocalPlayerInstance==null) {
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                Transform t = respawnPoints[Random.Range(0, respawnPoints.Length)];
                PhotonNetwork.Instantiate(this.playerPrefab.name, t.position, Quaternion.identity, 0);
            }else {
                // Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
        this.curAttack = 0;
        this.curCooldown = bulletPrefabs[curAttack].GetComponent<AttackStats>().cooldown;
    }

    public override void OnPlayerEnteredRoom( Player other  ) {
        Debug.Log( "OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting
        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom( Player other ) {
        Debug.Log( "OnPlayerLeftRoom() " + other.NickName ); // seen when other disconnects

        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            LoadArena(); 

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1){
                Debug.Log("WIN");
                showWin();
            }
        }
    }

    public override void OnLeftRoom() {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }
    public void QuitApplication() {
        Application.Quit();
    }

    void LoadArena() {
        if ( !PhotonNetwork.IsMasterClient ) {
            Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
        }
        Debug.LogFormat( "PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount );
        //PhotonNetwork.LoadLevel("MultiPlayer");
    }
    

    public static void SpawnBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        GameObject bulletInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "OnlineBullet"), spawnPos, rotation);
        bulletInstance.GetComponent<BulletScript>().isOnline = true;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * bulletInstance.GetComponent<AttackStats>().bulletSpeed;
        //Destroy(bulletInstance,DeathTime);
        //StartCoroutine(bulletInstance.GetComponent<BulletScript>().NetworkDestroyEnum(DeathTime));
    }

    public static void SpawnTurretBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        GameObject bulletInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TurretBullet"), spawnPos, rotation);
        bulletInstance.GetComponent<BulletScript>().isOnline = true;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * bulletInstance.GetComponent<AttackStats>().bulletSpeed;
        //Destroy(bulletInstance,DeathTime);
        //StartCoroutine(bulletInstance.GetComponent<BulletScript>().NetworkDestroyEnum(DeathTime));
    }

    public void showWin(){
        StartCoroutine (showWinEnum());
    }

    IEnumerator showWinEnum(){
        winText.SetActive(true);
        yield return new WaitForSeconds(3);

        PhotonNetwork.Disconnect ();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene("TitleScreen");
    }

    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */

    
}
