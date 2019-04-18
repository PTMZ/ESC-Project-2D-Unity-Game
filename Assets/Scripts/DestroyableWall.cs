using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float health = 40;

    public Sprite[] spriteList;
    public int curSprite = 4;

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
            go.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
        }
    }

    public void changeColour(int newColour)
    {
        curSprite = newColour;
        GetComponent<SpriteRenderer>().sprite = spriteList[curSprite];
    }
}
