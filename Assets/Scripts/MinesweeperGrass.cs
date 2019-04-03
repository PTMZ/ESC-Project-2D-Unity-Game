using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperGrass : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        mySpriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(mySpriteRenderer.bounds.min).y * -1;
    }
}
