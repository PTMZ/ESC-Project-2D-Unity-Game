using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SinglePlayerButton : MonoBehaviour
{
    public GameObject NewGameBut;
    public GameObject ContinueBut;
    public GameObject SinglePlayerBut;
    public GameObject MultiplayerBut;
    public int count = 0;


    void Start()
    {
        if (count == 0)
        {
            FindObjectOfType<AudioManager>().Play("TitleScreen");
            count++;
        }

    }

    public void toPrologue()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        StartCoroutine(wait(2));

    }

    public void continueFromSave()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        return;//to add save data here.
    }

    public void ClickSinglePlayer()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SinglePlayerBut.SetActive(false);
        MultiplayerBut.SetActive(false);
        ContinueBut.SetActive(true);
        NewGameBut.SetActive(true);
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
