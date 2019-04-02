using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralPortal : MonoBehaviour
{
    public string curSceneTheme;
    public string nextSceneTheme;
    public string nextSceneName;

    void Start(){
        Debug.Log("Portal Start code");
        Debug.Log(AudioManager.instance.curTheme);
        Debug.Log(string.Compare(AudioManager.instance.curTheme, ""));
        if(string.Compare(AudioManager.instance.curTheme, "") == 0){
            Debug.Log("Start Theme");
            AudioManager.instance.curTheme = curSceneTheme;
            AudioManager.instance.Play(curSceneTheme);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerAvatar>().getIsDead()){
                return;
            }
            AudioManager.instance.Play("EnterPortal");
            if(string.Compare(AudioManager.instance.curTheme, nextSceneTheme) != 0){
                AudioManager.instance.Stop(AudioManager.instance.curTheme);
                AudioManager.instance.Play(nextSceneTheme);
            }
            SceneManager.LoadScene(nextSceneName);
        }
        
    }
}
