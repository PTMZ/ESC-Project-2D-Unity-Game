using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class prologueDialogueManager : MonoBehaviour
{
    //public Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;
    //AnimatorClipInfo[] m_CurrentClipInfo;
    //string m_ClipName;


    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
       // animator = gameObject.GetComponent<Animator>();
        //m_CurrentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        //m_ClipName = m_CurrentClipInfo[0].clip.name;

    }
    public void StartDialogue(Dialogue dialogue)
    {
        //animator.setBool("IsOpen",true);

        //        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            animator.SetBool("readFinish", true);
            StartCoroutine(wait(10));
            
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            for (int i = 0; i < 3; i++)
            {
                yield return null;
            }
        }
    }

    void EndDialogue()
    {

        SceneManager.LoadScene("B5_AVHQ");
        //animator.setBool("IsOpen", false);
        //Debug.Log("End of conversation.");
    }

    private IEnumerator wait(float Time)
    {
        Time = Time / 6;
        yield return new WaitForSeconds(Time);
        EndDialogue();
        //if (gameObject != null)
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}

    }
}
