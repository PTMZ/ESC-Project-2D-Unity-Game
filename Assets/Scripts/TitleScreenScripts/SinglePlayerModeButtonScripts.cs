using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayerModeButtonScripts : MonoBehaviour
{
    public void toPrologue()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        Debug.Log("New game button is clicked.");
        OfflineGameManager.instance.storyProgress = 0;
        //StartCoroutine(wait(2));
        StopAllCoroutines();
        SceneManager.LoadScene("Prologue");

    }

    public void continueFromSave()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SaveGameScript.LoadGame();
        return;
    }

    private IEnumerator wait(float Time)
    {
        Time = Time / 3;
        yield return new WaitForSeconds(Time);
        Debug.Log("Finished wait");
        SceneManager.LoadScene("Prologue");
        //if (gameObject != null)
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}

    }
}
