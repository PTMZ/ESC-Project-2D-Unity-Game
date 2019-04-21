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

    public void getHit(float damage)
    {
        health -= damage;
        if (health <= 0 && SceneManager.GetActiveScene().name == "B4_AVHQ")
        {
            if(isHPBox){
                Instantiate(HPPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (health <= 0 && SceneManager.GetActiveScene().name == "L2_AVHQ")
        {
            if (isHPBox)
            {
                Instantiate(KeyPrefab, transform.position, Quaternion.identity);
            }
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

    public void changeColour(int newColour){
        curSprite = newColour;
        GetComponent<SpriteRenderer>().sprite = spriteList[curSprite];
    }
}
