using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{

    public Animator animator;
    private SpriteRenderer mySpriteRenderer;
    public Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        change = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {

        if (change != Vector3.zero)
        {
            mySpriteRenderer.flipX = (change.x < 0);
            animator.SetBool("moving", true);
            //Debug.Log("i am moving");
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
