using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletScript : MonoBehaviourPunCallbacks
{

    public float impactRadius = 2;
    public float impactPower = 5;
    public float DeathTime = 2f;
    float bulletDmg = 10;
    public bool isOnline = true;

    // Start is called before the first frame update
    void Start()
    {
        if(isOnline){
            StartCoroutine(NetworkDestroyEnum(DeathTime));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        if(col.gameObject.GetComponent<PlayerMovement>() != null){
            return;
        }
        */
        if(col.gameObject.GetComponent<PlayerAvatar>() != null){
            Debug.Log("I am hit, my name is " + col.gameObject.name);
            col.gameObject.GetComponent<PlayerAvatar>().getHit(bulletDmg);
        }
        if(col.gameObject.GetComponent<EnemyAvatar>() != null){
            //Debug.Log("Enemy hit is " + col.gameObject.name);
            col.gameObject.GetComponent<EnemyAvatar>().getHit(bulletDmg);
        }
        //Debug.Log("OnCollisionEnter2D");
        Vector2 hitPoint = col.GetContact(0).point;
        Rigidbody2D other = col.otherRigidbody;
        AddExplosionForce(other, impactPower, new Vector3(hitPoint.x, hitPoint.y, 0), impactRadius);
        //Destroy(gameObject);
        if(isOnline){
            PhotonNetwork.Destroy(gameObject);
        }
        else{
            Destroy(gameObject);
        }

    }

    public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius){
            var dir = (body.transform.position - expPosition);
            float calc = 1 - (dir.magnitude / expRadius);
            if (calc <= 0) {
                    calc = 0;		
            }

            body.AddForce (dir.normalized * expForce * calc);
    }

    private IEnumerator NetworkDestroyEnum(float DeathTime)
    {
        yield return new WaitForSeconds(DeathTime);
        if (gameObject != null){
             PhotonNetwork.Destroy(gameObject);
         }
       
    }

}
