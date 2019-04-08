using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QnsMarkScript : MonoBehaviour
{
    private Animator animator;
    public static bool isAnimating = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (!isAnimating)
        {
            //animator.SetBool("setQnMark", true);
            isAnimating = false;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("setQnMark", false);
        //if (!isAnimating)
        //{
        //    animator.SetBool("setQnMark", true);
        //    isAnimating = false;
        //}
        //else if (isAnimating)
        //{
        //    animator.SetBool("setQnMark", false);
        //    isAnimating = false;
        //}

    }
}
