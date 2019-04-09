using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float health = 40;

    public void getHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (FloatingTextPrefab)
        {
            ShowFloatingTextHealth();
        }

        void ShowFloatingTextHealth()
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
