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
    public GameObject[] bulletPrefabs;

    [HideInInspector]
    public PlayerAvatar LocalPlayer;
    [HideInInspector]
    public float curDamage;
    [HideInInspector]
    public float curBulletSpeed;
    [HideInInspector]
    public float curCooldown;

    public int curAttack;

    void Awake(){
        
    }

    void Start(){
        //PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        //LocalPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity).GetComponent<PlayerAvatar>();
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        //curAttack = 0;
        UpdateWeapon(curAttack);
        
    }

    public void UpdateWeapon(int newAttack){
        curAttack = newAttack;
        curDamage = bulletPrefabs[curAttack].GetComponent<AttackStats>().damage;
        curBulletSpeed = bulletPrefabs[curAttack].GetComponent<AttackStats>().bulletSpeed;
        curCooldown = bulletPrefabs[curAttack].GetComponent<AttackStats>().cooldown;
    }

    public void SpawnBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        GameObject bulletInstance = Instantiate(bulletPrefabs[curAttack], spawnPos, rotation);
        bulletInstance.GetComponent<BulletScript>().isOnline = false;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * curBulletSpeed;
    }

    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */

    
}
