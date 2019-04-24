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
    public GameObject dialogueButton;
    //public Animator animator;
    public GameObject pauseButton;
    public GameObject gameControls;
    public GameObject countTap;
    public static DialogueManager instance;
    private int clickNum = 0;



    private Queue<string> sentences;
    private Queue<string> names;

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

        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //animator.setBool("IsOpen",true);

        //nameText.text = dialogue.name.First();
        dialogueBox.SetActive(true);
        sentences.Clear();
        names.Clear();

        foreach (string name in dialogue.name)
        {
            names.Enqueue(name);
        }
        //string name1 = names.Dequeue();
        //nameText.text = name1;
        foreach (string sentence in dialogue.sentences)
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
        string name = names.Dequeue();
        nameText.text = name + ":";
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        cantContinue();
        startClickCount();
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (clickNum == 1)
            {
                continue;
            }
            else
            {
                yield return null;
            }
        }
        stopClickCount();
        canContinue();
    }

    public void incrementclickNum()
    {
        clickNum++;
    }

    void cantContinue()
    {
        dialogueButton.SetActive(false);
    }
    void canContinue()
    {
        dialogueButton.SetActive(true);
    }
    void startClickCount()
    {
        clickNum = 0;
        countTap.SetActive(true);
    }
    void stopClickCount()
    {
        clickNum = 0;
        countTap.SetActive(false);
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
        pauseButton.SetActive(true);
        gameControls.SetActive(true);
        Time.timeScale = 1.0f;
    }

    void timeStop()
    {
        Debug.Log("Game time stoppped for dialogue.");
        pauseButton.SetActive(false);
        gameControls.SetActive(false);
        Time.timeScale = 0.0f;
    }


}
