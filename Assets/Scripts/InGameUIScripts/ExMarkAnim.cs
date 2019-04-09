using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMarkAnim : MonoBehaviour
{
    public Animator puncMarkAnimator;
    //public SpriteRenderer qnMarkRenderer;
    //public SpriteRenderer exMarkRenderer;

    // Start is called before the first frame update
    void Start()
    {
        puncMarkAnimator = GetComponent<Animator>();
        //qnMarkRenderer = GetComponent<SpriteRenderer>();
        //exMarkRenderer = GetComponent<SpriteRenderer>();

    }
}
