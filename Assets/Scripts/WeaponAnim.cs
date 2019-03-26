using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{

    //public Vector3 change;
    public Animator weaponAnimator;
    public SpriteRenderer mySpriteRenderer;
    //private Vector3 enemyChange;

    // Start is called before the first frame update
    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //enemyChange = GetComponent<EnemyAvatar>().change;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //mySpriteRenderer.flipX = (enemyChange.x < 0);
        //UpdateAnimation();
        
    }

    void UpdateAnimation()
    {
        /*

        if (enemyChange != Vector3.zero)
        //if (enemy.animator.GetBool("moving") == true) 
        {
            //mySpriteRenderer.flipX = (enemy.change.x < 0);
            weaponAnimator.SetBool("weapmoving", true);
        }
        else
        {
            weaponAnimator.SetBool("weapmoving", false);
        }

        */
    }
}
