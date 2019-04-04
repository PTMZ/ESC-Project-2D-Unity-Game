using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InGameUIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameControls;

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        gameControls.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        gameControls.SetActive(true);
    }

    public void ReturnToTitleScreen()
    {
        AudioManager.instance.StopAll();
        Destroy(AudioManager.instance);
        Destroy(OfflineGameManager.instance);
        SceneManager.LoadScene("TitleScreen");
    }
}
