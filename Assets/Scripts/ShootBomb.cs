using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    public float bombDamage = 100;
    public float explosionRate = 1f;
    public float explosionMaxSize = 1f;
    public float explosionSpeed = 10f;
    public float explosionForce = 10f;
    public float currentRadius = 0.1f;
    public float health = 30;
    public Animator animator;
    public GameObject FloatingTextPrefab;

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


    public void getHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            exploded = true;
            animator.Play("bomb_explosion", -1);
            //Destroy(gameObject,0.2f);
        }

        if (FloatingTextPrefab)
        {
            ShowFloatingTextHealth();
        }

        void ShowFloatingTextHealth()
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
        }
    }

   


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() != null)
        {
            animator.Play("bomb_explosion", -1);
            //Destroy(gameObject, 0.2f);

        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction1 = explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);
        }

    }
    


}
