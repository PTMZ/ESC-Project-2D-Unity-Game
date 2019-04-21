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

    public void getHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);

            if (SceneManager.GetActiveScene().name == "B4_AVHQ")
            {
                if (isHPBox)
                {
                    Instantiate(HPPrefab, transform.position, Quaternion.identity);
                }
            }

            if (SceneManager.GetActiveScene().name == "L2_AVHQ" || SceneManager.GetActiveScene().name == "L3_AVHQ")
            {
                if (isKeyBox)
                {
                    Instantiate(KeyPrefab, transform.position, Quaternion.identity);
                }

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
