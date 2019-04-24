using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyable : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public GameObject HPPrefab;
    public GameObject KeyPrefab;
    public float health = 40;

    public Sprite[] spriteList;
    public int curSprite = 4;
    public bool isHPBox = false;
    public bool isKeyBox = false;
    public int defence = 0;
    public void getHit(float damage)
    {
        health -= damage - defence;
        if (health <= 0)
        {
            Destroy(gameObject);

            if (isHPBox)
            {
                Instantiate(HPPrefab, transform.position, Quaternion.identity);
            }

            if (isKeyBox)
            {
                Instantiate(KeyPrefab, transform.position, Quaternion.identity);
            }

        }

        if (FloatingTextPrefab)
        {
            ShowFloatingTextHealth();
        }


    }


    void ShowFloatingTextHealth()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMPro.TextMeshPro>().text = health.ToString();
    }

    public void changeColour(int newColour){
        curSprite = newColour;
        GetComponent<SpriteRenderer>().sprite = spriteList[curSprite];
    }
}
