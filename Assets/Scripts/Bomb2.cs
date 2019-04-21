using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2 : MonoBehaviour
{
    public float impactRadius = 2;
    public float impactPower = 5;
        public GameObject explosion;
    public Animator animator;
    public float bombDamage = 100;
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
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
        /*
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && exploded == true)
        {
            Vector2 target = collision.gameObject.transform.position;
            Vector2 bomb = gameObject.transform.position;
            Vector2 direction1 = explosionForce * (target - bomb);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction1);
        }
        */

        //Vector2 hitPoint = collision.GetContact(0).point;
        //Rigidbody2D other = collision.otherRigidbody;
       //AddExplosionForce(other, impactPower, new Vector3(hitPoint.x, hitPoint.y, 0), impactRadius);
        //Destroy(gameObject);
        GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp, 0.3f);


    }

    public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
    {
        var dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0)
        {
            calc = 0;
        }

        body.AddForce(dir.normalized * expForce * calc);
    }

}
