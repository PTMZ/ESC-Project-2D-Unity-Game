using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{

    public PlayerAvatar PlayerPrefab;

    [HideInInspector]
    public PlayerAvatar LocalPlayer;

    void Awake(){
        if(!PhotonNetwork.IsConnected){
            SceneManager.LoadScene("TitleScreen");
            return;
        }
    }

    void Start(){
        //PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        LocalPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity).GetComponent<PlayerAvatar>();
        if (PhotonNetwork.IsMasterClient){
            //Debug.Log("IS_MASTER");
            //PhotonNetwork.InstantiateSceneObject("Car", new Vector3(0, 1, 20), Quaternion.identity);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MoveSquare"), new Vector3(2, -2, 0), Quaternion.identity);
        }
    }
    

    public static void SpawnBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector, float DeathTime){
        GameObject bulletInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bullet"), spawnPos, rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector;

        //Destroy(bulletInstance,DeathTime);
        //StartCoroutine(bulletInstance.GetComponent<BulletScript>().NetworkDestroyEnum(DeathTime));
    }
    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */

    
}
