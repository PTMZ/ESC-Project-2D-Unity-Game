using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{   //areaSounds, playerSouds, enemySounds
    public Sound[] areaSounds;
    public Sound[] playerSounds;
    public Sound[] enemySounds;
    public Sound[] soundOnLoopButMustStop;

    public static AudioManager instance;
    public string curTheme = "";

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        //AudioManager.instance.Play("xyz") //this code can be used to replace FindObjectOfType<AudioManager>().Play("xyz") 
        foreach (Sound s in areaSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.isPlaying = s.isPlaying;
        }
        foreach (Sound s in playerSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.isPlaying = s.isPlaying;
        }
        foreach (Sound s in enemySounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.isPlaying = s.isPlaying;
        }
        foreach (Sound s in soundOnLoopButMustStop)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.isPlaying = s.isPlaying;
        }
    }
    
    public void PlayTheme(string name){
        curTheme = name;
        Sound s1 = Array.Find(areaSounds,sound => sound.name==name);

        if (s1 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found in areaSounds.");
        }
        else
        {
            s1.source.Play();
            DontDestroyOnLoad(s1.source);
        }
    }

    public void PlaySound(string name){
        Sound s2 = Array.Find(playerSounds, sound => sound.name == name);
        if (s2 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found playerSounds.");
        }
        else
        {
            s2.source.Play();
        }

        Sound s3 = Array.Find(enemySounds, sound => sound.name == name);
        if (s3 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found enemySounds.");
            return;
        }
        else
        {
            s3.source.Play();
        }
    }

    public void Play(string name)
    {
        Sound s1 = Array.Find(areaSounds,sound => sound.name==name);
        if (s1 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found in areaSounds.");
        }
        else
        {
            if (s1.source.isPlaying)
                return;
            s1.source.Play();
        }

        Sound s2 = Array.Find(playerSounds, sound => sound.name == name);
        if (s2 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found playerSounds.");
        }
        else
        {
            s2.source.Play();
        }

        Sound s3 = Array.Find(enemySounds, sound => sound.name == name);
        if (s3 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found enemySounds.");
            return;
        }
        else
        {
            s3.source.Play();
        }

        Sound s4 = Array.Find(soundOnLoopButMustStop, sound => sound.name == name);
        if (s4 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found soundonloopbutmuststop.");
            return;
        }
        else
        {
            //Debug.LogWarning("Sound: " + name + " is found soundonloopbutmuststop.");
            s4.source.Play();
        }
    }

    public void PlayLoopButMustStop(string name)
    {
        Sound L = Array.Find(soundOnLoopButMustStop, sound => sound.name == name);
        if (L == null)
        {
            Debug.LogWarning("Sound: " + name + " is not found soundonloopbutmuststop.");
            return;
        }
        else
        {
            //Debug.LogWarning("Sound: " + name + " is found soundonloopbutmuststop.");
            L.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound z1 = Array.Find(areaSounds, sound => sound.name == name);
        if (z1 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found in areaSounds.");
        }
        else
        {
            z1.source.Stop();
        }

        Sound z2 = Array.Find(playerSounds, sound => sound.name == name);
        if (z2 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found playerSounds.");
        }
        else
        {
            z2.source.Stop();
        }

        Sound z3 = Array.Find(enemySounds, sound => sound.name == name);
        if (z3 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found enemySounds.");
            return;
        }
        else
        {
            z3.source.Stop();
        }

        Sound z4 = Array.Find(soundOnLoopButMustStop, sound => sound.name == name);
        if (z4 == null)
        {
            //Debug.LogWarning("Sound: " + name + " is not found enemySounds.");
            return;
        }
        else
        {
            z4.source.Stop();
        }
        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


    }

    //currently only stops areasounds
    public void StopAll()
    {
        foreach (Sound s in areaSounds)
        {
            try
            {
                s.source.Stop();
            }
            catch (Exception e)
            {

            }
        }
    }

    public void StopMustStopSFX()
    {
        if(soundOnLoopButMustStop.Length > 0)
        {
            foreach (Sound s in soundOnLoopButMustStop)
            {
                try
                {
                    s.source.Stop();
                }
                catch (Exception e)
                {

                }
            }
        }

    }


}
