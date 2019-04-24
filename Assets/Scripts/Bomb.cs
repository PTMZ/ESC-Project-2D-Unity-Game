using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    //public float explosionMaxSize = 1f;
    //public float explosionSpeed = 10f;
    //public float explosionRate = 1f;
    //public float currentRadius = 0.1f;
    public float bombDamage = 100;
    public float impactRadius = 2;
    public float impactPower = 5;
    public float explosionForce = 100f;
    public float expRadius = 10f;       
    public Animator animator;

    bool exploded = false;
    BoxCollider2D explosionRadius;

    private List<Collider2D> colliders = new List<Collider2D>();

    void Start()
    {
        explosionRadius = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    /*
    void FixedUpdate()
    {
        if (exploded == true)
        {
            if (currentRadius < explosionMaxSize)
            {
                currentRadius += explosionRate;
            }
            else
            {
                Destroy(gameObject, 0.2f);
            }
            explosionRadius.edgeRadius = currentRadius;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (!colliders.Contains(collision)) {
            colliders.Add(collision);
        }

        if (collision.gameObject.GetComponent<PlayerAvatar>() != null)
        {
            ApplyForceToAll();
            exploded = true;
            animator.Play("bomb_explosion", -1);
            collision.gameObject.GetComponent<PlayerAvatar>().getHit(bombDamage);
            AudioManager.instance.Play("BombExplosion");
            Destroy(gameObject, 0.3f);
        }

        if (collision.gameObject.GetComponent<EnemyAvatar>() != null)
        {
            collision.gameObject.GetComponent<EnemyAvatar>().getHit(25);
            animator.Play("bomb_explosion", -1);
            AudioManager.instance.Play("BombExplosion");
            Destroy(gameObject, 0.3f);
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {

            /*
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 dir =  explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);

            float calc = 1 - (dir.magnitude / expRadius);
            if (calc <= 0)
            {
                calc = 0;
            }

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * explosionForce * calc);
            */


            //Rigidbody2D other = collision.gameObject.GetComponent<Rigidbody2D>();
            //AddExplosionForce(other, impactPower, gameObject.transform.position, impactRadius);
            //Destroy(gameObject);
            if(bomb){
                GameObject exp = Instantiate(bomb, transform.position, transform.rotation);
                Destroy(exp, 0.3f);
            }
        }

    }

     private void OnTriggerExit2D (Collider2D collision) {
         //colliders.Remove(collision);
     }

    
    private void ApplyForceToAll(){
        Debug.Log("EXPLODE");
        foreach(Collider2D col in colliders){
            if(col){
                Rigidbody2D other = col.gameObject.GetComponent<Rigidbody2D>();
                if(other){
                    Debug.Log("EXPLODE: " + col.gameObject.name);
                    AddExplosionForce(other, impactPower, gameObject.transform.position, impactRadius);
                }
            }
        }
    }

    public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
    {
        var dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0)
        {
            calc = 0;
        }
        Debug.Log("EXP FORCE: " + dir.normalized * expForce * calc);
        body.AddForce(dir.normalized * expForce * calc, ForceMode2D.Impulse);
    }
    




}
