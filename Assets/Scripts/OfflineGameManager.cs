using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class OfflineGameManager : MonoBehaviour
{
    public PlayerAvatar PlayerPrefab;
    public GameObject[] bulletPrefabs;
    public GameObject enemyBulletPrefab;
    public GameObject arrowPrefab;
    

    [HideInInspector]
    public float curHealth = 20;
    [HideInInspector]
    public static float maxHealth = 50;
    [HideInInspector]
    public float curDamage;
    [HideInInspector]
    public float curBulletSpeed;
    [HideInInspector]
    public float curCooldown;
    [HideInInspector]
    public int storyProgress;

    [HideInInspector]
    public float moveMult = 1;
    [HideInInspector]
    public float dmgMult = 1;

    public int curAttack;
    [HideInInspector]
    public Dictionary<string, int> spawnPoints = new Dictionary<string, int>()
    {
        {"B2_AVHQ", -1}, {"L1_AVHQ", -1}, {"L2_AVHQ", -1}, {"L3_AVHQ", -1}, {"L4_AVHQ", -1}, {"L5_AVHQ", -1},
        {"PW_01", -1}, {"PW_02", -1}, {"PW_03", -1}, {"PW_04", -1},
        {"MSP_01", -1}, {"MSP_02", -1}, {"MSP_03", -1}, {"MSP_04", -1}
    };
    

    public static OfflineGameManager instance;
    public AudioManager AMPrefab;

    void Awake(){
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if(AudioManager.instance == null){
            Debug.Log("Instantiate AM");
            Instantiate(AMPrefab, Vector3.zero, Quaternion.identity);
        }

        //Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        UpdateWeapon(curAttack);
    }

    void Start(){
        //PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        //LocalPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity).GetComponent<PlayerAvatar>();
        //curAttack = 0;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        
    }

    public void UpdateWeapon(int newAttack){
        curAttack = newAttack;
        curDamage = bulletPrefabs[curAttack].GetComponent<AttackStats>().damage;
        curBulletSpeed = bulletPrefabs[curAttack].GetComponent<AttackStats>().bulletSpeed;
        curCooldown = bulletPrefabs[curAttack].GetComponent<AttackStats>().cooldown;
    }

    public void UpdatePlayerStats(float m, float d){
        moveMult = m;
        dmgMult = d;
    }

    public void SpawnBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        var temp = Quaternion.FromToRotation(new Vector3(1,0,0), bulletVector);
        GameObject bulletInstance = Instantiate(bulletPrefabs[curAttack], spawnPos, temp);
        bulletInstance.GetComponent<BulletScript>().isOnline = false;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * curBulletSpeed;
    }

    public void SpawnEnemyBullet(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        var temp = Quaternion.FromToRotation(new Vector3(1,0,0), bulletVector);
        GameObject bulletInstance = Instantiate(enemyBulletPrefab, spawnPos, temp);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * curBulletSpeed;
    }

    public void SpawnArrow(Vector3 spawnPos, Quaternion rotation, Vector3 bulletVector){
        var temp = Quaternion.FromToRotation(new Vector3(1,0,0), bulletVector);
        GameObject bulletInstance = Instantiate(arrowPrefab, spawnPos, temp);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVector * curBulletSpeed;
    }

    public void respawnPlayer(GameObject obj)
    {
        Destroy(obj, 3);
        StartCoroutine(DelayLoad(2));
    }

    public void respawnEnemy(GameObject obj)
    {
        Destroy(obj, 0);
        //StartCoroutine(DelayLoad(2));
    } void destroyBomb(GameObject obj)
    {
        Destroy(obj, 0);
    }

    public void loadSceneAtDeath(string scene)
    {
        //SpawnManager spawnManager = GameObject.FindWithTag("SpawnManager");
        SceneManager.LoadScene(scene);
        //if (spawnPoints[SceneManager.GetActiveScene().name] == -1)
        //{
        //    SceneManager.LoadScene(scene);

        //}
        //else
        //{
            
        //}
        //StartCoroutine(DelayLoad(2));
    }

    private IEnumerator DelayLoad(float timing)
    {
        
        yield return new WaitForSeconds(timing);

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log(Application.persistentDataPath);
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // Update Game state
            OfflineGameManager.instance.curHealth = maxHealth;
            OfflineGameManager.instance.curAttack = save.curAttack;
            OfflineGameManager.instance.storyProgress = save.storyProgress;
            
            //DO NOT UNCOMMENT THIS UNLESS ANOTHER BUG APPEARS. AudioManager.instance.StopAll();
            loadSceneAtDeath(save.curScene);
            Debug.Log("Player Respawned.");
        }
        else
        {
            curHealth = maxHealth;
            loadSceneAtDeath(SceneManager.GetActiveScene().name);
        }
    }


    /*
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerAvatar.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }
    */


}
