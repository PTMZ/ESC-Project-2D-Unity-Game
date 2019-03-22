using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public AudioClip MusicClip;
    public AudioSource MusicSource;
    public static int startCount = 0;
    public static int awakeCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        MusicSource.clip = MusicClip;

        if (startCount == 0)
        {
            MusicSource.Play();
            startCount++;
        }
        
    }

    void Awake()
    {
        //string currentScene = SceneManager.GetActiveScene().name;
        if (awakeCount == 0)
        {
            DontDestroyOnLoad(this.gameObject);
            awakeCount++;
        }
    }
}
