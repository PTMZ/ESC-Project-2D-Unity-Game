using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punctuationMarkAnim : MonoBehaviour
{
    public Animator qnMarkAnimator;
    public Animator exMarkAnimator;
    //public SpriteRenderer qnMarkRenderer;
    //public SpriteRenderer exMarkRenderer;

    // Start is called before the first frame update
    void Start()
    {
        qnMarkAnimator = GetComponent<Animator>();
        exMarkAnimator = GetComponent<Animator>();
        //qnMarkRenderer = GetComponent<SpriteRenderer>();
        //exMarkRenderer = GetComponent<SpriteRenderer>();

    }
}
