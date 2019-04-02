using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;

    //public float explosionDelay = 1f;
    public float explosionRate = 10f;
    public float explosionMaxSize = 1f;
    public float explosionSpeed = 1f;
    public float explosionForce = 1000f;
    public float currentRadius = 1f;

    bool exploded = false;
    CircleCollider2D explosionRadius;

    void Start()
    {
        explosionRadius = gameObject.GetComponent<CircleCollider2D>();

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
            }
            else
            {
                Destroy(gameObject);
            }
            explosionRadius.radius = currentRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            exploded = true;
        }

        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction = explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
        }

    }


}
