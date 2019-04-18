using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyAvatar : MonoBehaviourPun, IPunObservable
{
    public GameObject FloatingTextPrefab;
    public float health;


    // for animations //
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    public Animator animator;
    private SpriteRenderer mySpriteRenderer;
    public WeaponAnim weaponAnim;
    private bool isDead = false;
    private bool facingLeft = false;
    public bool isMachineBoss = false;
    public Animator exMarkAnim;

    private OfflineGameManager offlineGM;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        weaponAnim = GetComponent<WeaponAnim>();
        change = Vector3.zero;
        //offlineGM = FindObjectOfType<OfflineGameManager>();
        offlineGM = OfflineGameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        
    }
    
    void UpdateAnimation(){


        if (change != Vector3.zero){

            if (change.x < 0 && facingLeft==false)
            {
                facingLeft = true;
                animator.transform.Rotate(0, 180, 0);
            }
            if (change.x > 0 && facingLeft == true)
            {
                facingLeft = false;
                animator.transform.Rotate(0, 180, 0);
            }

            //mySpriteRenderer.flipX = (change.x < 0);
            //weaponAnim.mySpriteRenderer.flipX = (change.x < 0);
            animator.SetBool("moving", true);
            weaponAnim.weaponAnimator.SetBool("weapmoving", true);
            


        }
        else{
            animator.SetBool("moving", false);
            weaponAnim.weaponAnimator.SetBool("weapmoving", false);
        }
    }
    

    public void getHit(float damage){
        if(isDead){
            //return;
        }
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            //transform.Rotate(0, 0, 90, Space.Self);
            animator.SetBool("dead", true);
            
            if (isMachineBoss)
            {
                Debug.Log("MACHINE IS DEAD");
                weaponAnim.weaponAnimator.SetBool("explode", true);
                //weaponAnim.weaponAnimator.Play("machine_explosion", -1);
                //weaponAnim.weaponAnimator.SetBool("hide", true);
                Destroy(gameObject, 1.3f);
            }
            else
            {
                weaponAnim.mySpriteRenderer.enabled = false;
            }
            
            
        }

        if (health < -30)
        {
            offlineGM.respawnEnemy(gameObject);
        }
        
        //Debug.Log("Enemy hit, health is = " + health);

        if (FloatingTextPrefab)
        {
            ShowFloatingText();
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        if (health >= 0)
        {
            go.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
        }
        else
        {
            go.GetComponent<TMPro.TextMeshPro>().text = null;
        }
        
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

    void LateUpdate()
    {
        mySpriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(mySpriteRenderer.bounds.min).y * -1;
    }

    public bool getDead(){
        return isDead;
    }

    public void TriggerExclamationMark()
    {
        exMarkAnim.Play("exmark", -1, 1.0f);
    }
}
