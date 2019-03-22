using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAvatar : MonoBehaviourPun, IPunObservable
{
    public GameObject FloatingTextPrefab;
    public float health = 100;


    // for animations //
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        change = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        Debug.Log(change);
        
    }
    
    void UpdateAnimation(){

        if (change != Vector3.zero){
            mySpriteRenderer.flipX = (change.x < 0);
            animator.SetBool("moving", true);
        }
        else{
            animator.SetBool("moving", false);
        }
    }
    

    public void getHit(float damage){
        if(isDead){
            return;
        }
        health -= damage;
        if(health<=0){
            isDead = true;
            transform.Rotate(0, 0, 90, Space.Self);
        }
        Debug.Log("Enemy hit, health is = " + health);

        if(FloatingTextPrefab)
        {
            ShowFloatingText();
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(change);
            stream.SendNext(health);
        }
        else{
            change = (Vector3)stream.ReceiveNext();
            UpdateAnimation();
            health = (float)stream.ReceiveNext();
        }

    }

    public bool getDead(){
        return isDead;
    }
}
