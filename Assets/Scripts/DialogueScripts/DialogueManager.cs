using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public TMP_Text dialogueText;

    //public Animator animator;

    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //animator.setBool("IsOpen",true);

        //        nameText.text = dialogue.name;
       
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        timeStop();
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {
        timeStart();
        //SceneManager.LoadScene("EscapeLevel");
        //animator.setBool("IsOpen", false);
        //Debug.Log("End of conversation.");
    }

    void timeStart()
    {
        Time.timeScale = 1.0f;
    }

    void timeStop()
    {
        Time.timeScale = 0.0f;
    }
}
