using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralPortal : MonoBehaviour
{
    public string curSceneTheme;
    public string nextSceneTheme;
    public string nextSceneName;
    public int spawnPoint;

    public AudioManager AMPrefab;

    void Start(){
        Debug.Log("Portal Start code");
        if(AudioManager.instance == null){
            Instantiate(AMPrefab);
        }
        Debug.Log(AudioManager.instance.curTheme);
        Debug.Log(string.Compare(AudioManager.instance.curTheme, ""));

        if (string.Compare(AudioManager.instance.curTheme, "") == 0)
        {
            Debug.Log("Start Theme");
            AudioManager.instance.curTheme = curSceneTheme;
            AudioManager.instance.Play(curSceneTheme);
        }

        Debug.Log("Start Theme");
        AudioManager.instance.curTheme = curSceneTheme;
        AudioManager.instance.Play(curSceneTheme);
        SaveGameScript.SaveGame();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerAvatar>().getIsDead()){
                return;
            }
            OfflineGameManager.instance.spawnPoints[SceneManager.GetActiveScene().name] = spawnPoint;
            AudioManager.instance.Play("EnterPortal");
            if(string.Compare(AudioManager.instance.curTheme, nextSceneTheme) != 0){
                AudioManager.instance.Stop(AudioManager.instance.curTheme);
                AudioManager.instance.Play(nextSceneTheme);
            }
            SceneManager.LoadScene(nextSceneName);
        }
        
    }
}
