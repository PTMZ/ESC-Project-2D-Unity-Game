using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAvatar : MonoBehaviourPun, IPunObservable
{
    public GameObject FloatingTextPrefab;
    public int points = 20;

    [HideInInspector]
    public float health;

    // for animations //
    private Rigidbody2D myRigidbody;
    public Vector3 change;
    private Animator animator;
    private SpriteRenderer mySpriteRenderer;
    private bool isDead = false;
    public Animator exMarkAnim;
    public Animator qnMarkAnim;

    private OfflineGameManager offlineGM;
    public OfflineGameManager OGMPrefab;
    private bool isOnline = false;

    public GameObject[] trails;
    public int curTrail = -1;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        isOnline = true;
        if (!photonView.IsMine && GetComponent<PlayerMovement>() != null)
        {
            Destroy(GetComponent<PlayerMovement>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // for animations //
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        change = Vector3.zero;
        //offlineGM = FindObjectOfType<OfflineGameManager>();
        if(OfflineGameManager.instance == null){
            Instantiate(OGMPrefab);
        }
        offlineGM = OfflineGameManager.instance;
        health = isOnline ? 100 : offlineGM.curHealth;


        foreach(GameObject t in trails){
            t.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            if (change != Vector3.zero)
            {
                mySpriteRenderer.flipX = (change.x < 0);
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }
    }

    void SyncAnimation()
    {

    }

    void FixedUpdate()
    {

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(change);
            stream.SendNext(health);
        }
        else
        {
            this.change = (Vector3)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();
            //UpdateAnimation();
        }

    }

    public void getPoints()
    {
        offlineGM.curHealth += points;
        if (offlineGM.curHealth >= OfflineGameManager.maxHealth)
        {
            offlineGM.curHealth = OfflineGameManager.maxHealth;
        }

        Debug.Log("get " + points + " points");
        if (FloatingTextPrefab)
        {
            ShowFloatingTextHealth(offlineGM.curHealth);
        }

    }

    void LateUpdate()
    {
        mySpriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(mySpriteRenderer.bounds.min).y * -1;
    }


    public void getHit(float damage)
    {
        if (isDead)
        {
            Debug.Log("DEAD alr");
            return;
        }
        if(isOnline){
            health -= damage;
            if(health <= 0){
                isDead = true;
                animator.SetBool("dead", true);
                if (photonView.IsMine  && GetComponent<PlayerMovement>() != null)
                {
                    Destroy(GetComponent<PlayerMovement>());
                }
            }
        }
        else{
            offlineGM.curHealth -= damage;
            if (offlineGM.curHealth <= 0)
            {
                isDead = true;
                animator.SetBool("dead", true);
                if ((photonView.IsMine || !PhotonNetwork.IsConnected) && GetComponent<PlayerMovement>() != null)
                {
                    Destroy(GetComponent<PlayerMovement>());
                }
                if(OnlineGameManager.Instance == null){
                    offlineGM.respawnPlayer(gameObject);
                }
                
            }
        }

        void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
        }

        void OnDisconnectedFromPhoton()
        {
            Debug.Log("OnPhotonPlayerDisconnected");

        }

        if (FloatingTextPrefab)
        {
            if(isOnline){
                ShowFloatingTextHealth(this.health);
            }
            else{
                ShowFloatingTextHealth(offlineGM.curHealth);
            }
        }

    }

    void ShowFloatingTextHealth(float h)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMPro.TextMeshPro>().text = h.ToString();
        if (h <= 0)
        {
            go.GetComponent<TMPro.TextMeshPro>().text = "-GAMEOVER-";

        }
    }

    public bool getIsDead(){
        return isDead;
    }


    public void TriggerExclamationMark()
    {
        //exMarkAnim.SetBool("setExMark", true);
        exMarkAnim.Play("exmark", -1, 1.0f);
        //exMarkAnim.SetBool("setExMark", false);
        //Debug.Log("Triggered ExMark");
        //StartCoroutine(setExFalseAfter(0.4f));

    }
    //IEnumerator setExFalseAfter(float x)
    //{
    //    WaitForSeconds waitForSeconds = new WaitForSeconds(x);
    //    yield return waitForSeconds;
    //    exMarkAnim.SetBool("setExMark", false);
    //}

    public void TriggerQuestionMark()
    {
        //qnMarkAnim.SetBool("setQnMark", true);
        //StartCoroutine(setQnFalseAfter(0.4f));
        qnMarkAnim.Play("qnsmark", -1, 1.0f);
        //WaitForRealSeconds(0.5f);
        //qnMarkAnim.SetBool("setQnMark", false);


    }
    //IEnumerator setQnFalseAfter(float x)
    //{
    //    WaitForSeconds waitForSeconds = new WaitForSeconds(x);
    //    yield return waitForSeconds;
    //    qnMarkAnim.SetBool("setQnMark", false);
    //}

    //IEnumerator _WaitForRealSeconds(float aTime)
    //{
    //    while (aTime > 0f)
    //    {
    //        aTime -= Mathf.Clamp(Time.unscaledDeltaTime, 0, 0.2f);
    //    }
    //    yield return null;
    //}
    //Coroutine WaitForRealSeconds(float aTime)
    //{
    //    return StartCoroutine(_WaitForRealSeconds(aTime));
    //}
    public void updateTrail(int trailNum)
    {
        if (curTrail != -1)
        {
            trails[curTrail].SetActive(false);
        }
        if (trailNum != -1)
        {
            trails[trailNum].SetActive(true);
        }
        curTrail = trailNum;
    }
    /*
    public static void RefreshInstance(ref PlayerAvatar player, PlayerAvatar prefab){
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        if(player != null){
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }
        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<PlayerAvatar>();
    }
    */
}
