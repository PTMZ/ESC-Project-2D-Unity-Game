using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class streamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public GameObject prologueVid;
    public prologueScript prologueText;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(PlayVideo());
        //prologue.SetActive(false);
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
        }

        rawImage.texture = videoPlayer.texture;
        //android URL
        //videoPlayer.url = "jar:file://" + Application.dataPath + "!/assets/";
        videoPlayer.Play();
        audioSource.Play();

        StartCoroutine(AfterPrologueVid());

    }

    IEnumerator AfterPrologueVid()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (videoPlayer.isPlaying)
        {
            yield return waitForSeconds;
        }
        prologueVid.SetActive(false);
        prologueText.TriggerDialogue();     
    }
    private IEnumerator wait(float Time)
    {
        Time = Time / 6;
        yield return new WaitForSeconds(Time);
    }
}
