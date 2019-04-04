using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PvEButton : MonoBehaviour
{
    public GameObject NewGameBut;
    public GameObject ContinueBut;
    public GameObject SinglePlayerBut;
    public GameObject MultiplayerBut;
    public int count;


    void Start()
    {
        if (count == 0)
        {
            FindObjectOfType<AudioManager>().Play("TitleScreen");
            count++;
            Debug.Log("Played once");
        }

    }

    public void ClickSinglePlayer()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SinglePlayerBut.SetActive(false);
        MultiplayerBut.SetActive(false);
        ContinueBut.SetActive(true);
        NewGameBut.SetActive(true);
    }


}
