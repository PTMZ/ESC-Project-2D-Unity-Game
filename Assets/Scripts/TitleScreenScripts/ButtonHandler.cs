using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("TitleScreen");
    }

    public void nextScene()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        StartCoroutine(wait(2));
        
    }
    private IEnumerator wait(float Time)
    {
        Time = Time / 3;
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene("Prologue");
        //if (gameObject != null)
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}

    }
}
