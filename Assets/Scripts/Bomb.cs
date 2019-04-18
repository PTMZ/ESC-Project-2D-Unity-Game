using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    public float bombDamage = 100;
    public float explosionRate = 1f;
    public float explosionMaxSize = 1f;
    public float explosionSpeed = 10f;
    public float explosionForce = 10f;
    public float currentRadius = 0.1f;
    public Animator animator;

    bool exploded = false;
    BoxCollider2D explosionRadius;

    void Start()
    {
        explosionRadius = gameObject.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            exploded = true;
            animator.Play("bomb_explosion", -1);
        }

        
        if (collision.CompareTag("Enemy"))
        {
            //exploded = false;
            //Debug.Log("HIT ENEMY");
            //animator.Play("bomb_explosion", -1);
        }
        
        if (collision.gameObject.GetComponent<PlayerAvatar>() != null)
        {
            collision.gameObject.GetComponent<PlayerAvatar>().getHit(bombDamage);

        }

        if (collision.gameObject.GetComponent<EnemyAvatar>() != null)
        {
            collision.gameObject.GetComponent<EnemyAvatar>().getHit(25);
            animator.Play("bomb_explosion", -1);
            //offlineGM.destroyBomb(gameObject);
            Destroy(gameObject, 0.2f);

        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction1 =  explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);
        }

    }


}
