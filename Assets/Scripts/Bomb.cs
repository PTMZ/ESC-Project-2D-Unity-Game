using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    public float bombDamage = 100;
    public float explosionRate = 10f;
    public float explosionMaxSize = 10f;
    public float explosionSpeed = 1000f;
    public float explosionForce = 1000f;
    public float currentRadius = 0.1f;

    public Animator animator;

    bool exploded = false;
    BoxCollider2D explosionRadius;

    void Start()
    {
        explosionRadius = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

    /*
    void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    */

    /*
    void Update()
    {
        explosionDelay -= Time.deltaTime;
        if (explosionDelay < 0)
        {
            exploded = true;
        }
    }
    */

    void FixedUpdate()
    {
        if (exploded == true)
        {
            if (currentRadius < explosionMaxSize)
            {
                currentRadius += explosionRate;
                //offlineGM.loadScene();
            }
            else
            {
                Destroy(gameObject);
                //SceneManager.GetActiveScene();
            }
            explosionRadius.edgeRadius = currentRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exploded = true;
            animator.Play("bomb_explosion", -1);
        }


        if (collision.gameObject.GetComponent<PlayerAvatar>() != null)
        {
            //Debug.Log("I am hit, my name is " + collision.gameObject.name);
            collision.gameObject.GetComponent<PlayerAvatar>().getHit(bombDamage);

        }

            if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction1 =  explosionForce * (target - bomb);
            Vector2 direction2 = - 0.5f * explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction2);
            //new WaitForSecondsRealtime(1);
            //Vector2 direction2 = -0.5f * explosionForce * (collision.gameObject.transform.position - gameObject.transform.position);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction2);
        }

    }


}
