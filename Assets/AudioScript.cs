using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public AudioClip MusicClip;
    public AudioSource MusicSource;

    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = MusicClip;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MusicSource.Play();
        }
    }
    void Awake()
    {
        int count = 0;
        //string currentScene = SceneManager.GetActiveScene().name;
        if (count == 0)
        {
            DontDestroyOnLoad(this.gameObject);
            count++;
        }
    }
}
