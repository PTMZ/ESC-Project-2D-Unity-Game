using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void nextScene()
    {
        SceneManager.LoadScene("EscapeLevel");
    }
}
