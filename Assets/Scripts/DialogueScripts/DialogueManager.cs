using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    //public Animator animator;

    public static DialogueManager instance;

    private Queue<string> sentences;

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
    }
        void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //animator.setBool("IsOpen",true);

        nameText.text = dialogue.name;
        dialogueBox.SetActive(true);
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

            for (int i = 0; i < 3; i++)
            {
                yield return null;
            }
        }
    }


    void EndDialogue()
    {
        timeStart();
        dialogueBox.SetActive(false);
        //SceneManager.LoadScene("EscapeLevel");
        //animator.setBool("IsOpen", false);
        //Debug.Log("End of conversation.");
    }

    void timeStart()
    {
        Debug.Log("Game time restarted at end of dialogue.");
        Time.timeScale = 1.0f;
    }

    void timeStop()
    {
        Debug.Log("Game time stoppped for dialogue.");
        Time.timeScale = 0.0f;
    }
}
